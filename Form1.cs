using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Song_Recommendation_App
{

    public partial class Form1 : Form
    {


        private const string clientId = "5a2b4100059a47caa9dbcbf59ca75394";
        private const string clientSecret = "2b83493dcadc4e1abf9521461c658d77";
        private const string redirectUri = "http://localhost:5000/callback/";
        private const string scopes = "user-read-private user-read-email playlist-modify-public playlist-modify-private\r\n";

        private string _refreshToken; // To store the refresh token

        public Form1()
        {
            InitializeComponent();
        }

        private async void login_Click(object sender, EventArgs e)
        {
            Monitoring.StartNewOperation(); 
            Monitoring.LogInfo("User clicked Login.");

            try
            {
                string authUrl = $"https://accounts.spotify.com/authorize?response_type=code&client_id={clientId}&response_type=code&scope={Uri.EscapeDataString(scopes)}&redirect_uri={Uri.EscapeDataString(redirectUri)}";

                Process.Start(new ProcessStartInfo
                {
                    FileName = authUrl,
                    UseShellExecute = true
                });

                var authHandler = new SpotifyAuth(redirectUri);
                string authorizationCode = await authHandler.GetAuthorizationCodeAsync();

                if (!string.IsNullOrEmpty(authorizationCode))
                {
                    Monitoring.LogInfo("Authorization code received successfully.");

                    var (accessToken, refreshToken) = await ExchangeCodeForTokens(authorizationCode);

                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        Monitoring.LogInfo("Access token obtained successfully.");
                        _refreshToken = refreshToken;
                        await HandleSpotifyLogin(accessToken);
                    }
                    else
                    {
                        Monitoring.LogWarning("Access token is null or empty after token exchange.");
                        MessageBox.Show("Failed to exchange authorization code for tokens.");
                    }
                }
                else
                {
                    Monitoring.LogWarning("Authorization failed. No code received.");
                    MessageBox.Show("Authorization failed. No code received.");
                }
            }
            catch (Exception ex)
            {
                Monitoring.LogError("An error occurred during Spotify login.", ex);
                MessageBox.Show("An unexpected error occurred during login.");
            }
        }

        private async Task<(string accessToken, string refreshToken)> ExchangeCodeForTokens(string authorizationCode)
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

                var postData = new StringBuilder();
                postData.Append($"grant_type=authorization_code");
                postData.Append($"&code={authorizationCode}");
                postData.Append($"&redirect_uri={Uri.EscapeDataString(redirectUri)}");
                postData.Append($"&client_id={clientId}");
                postData.Append($"&client_secret={clientSecret}");

                request.Content = new StringContent(postData.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var json = JsonDocument.Parse(responseString);
                        string accessToken = json.RootElement.GetProperty("access_token").GetString();
                        string refreshToken = json.RootElement.GetProperty("refresh_token").GetString();
                        return (accessToken, refreshToken);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing token response: {ex.Message}\nResponse: {responseString}");
                        return (null, null);
                    }
                }
                else
                {
                    MessageBox.Show($"Failed to get tokens:\n{responseString}");
                    return (null, null);
                }
            }
        }

        private async Task<string> RefreshAccessToken()
        {
            if (string.IsNullOrEmpty(_refreshToken))
            {
                MessageBox.Show("No refresh token available.");
                return null;
            }

            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");

                var postData = new StringBuilder();
                postData.Append($"grant_type=refresh_token");
                postData.Append($"&refresh_token={_refreshToken}");
                postData.Append($"&client_id={clientId}");
                postData.Append($"&client_secret={clientSecret}");

                request.Content = new StringContent(postData.ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");

                var response = await client.SendAsync(request);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var json = JsonDocument.Parse(responseString);
                        string newAccessToken = json.RootElement.GetProperty("access_token").GetString();
                      
                        if (json.RootElement.TryGetProperty("refresh_token", out var newRefreshTokenElement))
                        {
                            _refreshToken = newRefreshTokenElement.GetString();
                        }
                        return newAccessToken;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing refresh token response: {ex.Message}\nResponse: {responseString}");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show($"Failed to refresh token:\n{responseString}");
                    return null;
                }
            }
        }

        private async Task GetCurrentUserProfile(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var response = await client.GetAsync("https://api.spotify.com/v1/me");

                var responseString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var json = JsonDocument.Parse(responseString);
                        string displayName = json.RootElement.GetProperty("display_name").GetString();
                        string email = json.RootElement.GetProperty("email").GetString();

                        MessageBox.Show($"Logged in as: {displayName}\nEmail: {email}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error parsing user profile response: {ex.Message}\nResponse: {responseString}");
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                   
                    string newAccessToken = await RefreshAccessToken();
                    if (!string.IsNullOrEmpty(newAccessToken))
                    {
                        await GetCurrentUserProfile(newAccessToken);  }
                    else
                    {
                        MessageBox.Show("Failed to refresh access token. Please log in again.");
                    }
                }
                else
                {
                    MessageBox.Show($"Failed to get user profile:\n{responseString}");
                }
            }
        }

        public User GetUserFromDatabase(string spotifyUserId)
        {
            using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT SpotifyUserId, Email, DisplayName, AccessToken, RefreshToken FROM Users WHERE SpotifyUserId = @SpotifyUserId";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SpotifyUserId", spotifyUserId);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new User
                                {
                                    SpotifyUserId = reader["SpotifyUserId"].ToString(),
                                    Email = reader["Email"].ToString(),
                                    DisplayName = reader["DisplayName"].ToString(),
                                    AccessToken = reader["AccessToken"].ToString(),
                                    RefreshToken = reader["RefreshToken"].ToString() // Retrieve refresh token
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error accessing database: {ex.Message}");
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
            return null;
        }

        public void SaveUserToDatabase(User user)
        {
            using (SqlConnection conn = new SqlConnection(Config.ConnectionString))
            {
                try
                {
                    conn.Open();
                    string query = @"
                IF NOT EXISTS (SELECT 1 FROM Users WHERE SpotifyUserId = @SpotifyUserId)
                INSERT INTO Users (SpotifyUserId, Email, DisplayName, AccessToken, RefreshToken, TokenExpiration)
                VALUES (@SpotifyUserId, @Email, @DisplayName, @AccessToken, @RefreshToken, DATEADD(hour, 1, GETDATE()))
                ELSE
                UPDATE Users SET AccessToken = @AccessToken, RefreshToken = @RefreshToken, TokenExpiration = DATEADD(hour, 1, GETDATE())
                WHERE SpotifyUserId = @SpotifyUserId";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@SpotifyUserId", user.SpotifyUserId);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@DisplayName", (object)user.DisplayName ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@AccessToken", user.AccessToken);
                        cmd.Parameters.AddWithValue("@RefreshToken", user.RefreshToken); // Save refresh token

                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving user to database: {ex.Message}");
                }
                finally
                {
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }


        public string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(Config.JwtSecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
        new Claim("SpotifyUserId", user.SpotifyUserId),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique ID for the token
    };

            var token = new JwtSecurityToken(
                issuer: "YourAppName",
                audience: "YourAppNameAudience",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<User> GetSpotifyUserProfile(string accessToken)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                var response = await client.GetAsync("https://api.spotify.com/v1/me");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    try
                    {
                        dynamic json = JsonConvert.DeserializeObject(content);
                        User user = new User
                        {
                            SpotifyUserId = json.id,
                            DisplayName = json.display_name,
                            Email = json.email
                        };
                        return user;
                    }
                    catch (JsonSerializationException ex)
                    {
                        MessageBox.Show($"Error deserializing user profile: {ex.Message}\nContent: {content}");
                        return null;
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    string newAccessToken = await RefreshAccessToken();
                    if (!string.IsNullOrEmpty(newAccessToken))
                    {
                        return await GetSpotifyUserProfile(newAccessToken);
                    }
                    else
                    {
                        MessageBox.Show("Failed to refresh access token while fetching user profile. Please log in again.");
                        return null;
                    }
                }
                else
                {
                    MessageBox.Show($"Failed to fetch Spotify user profile. Status Code: {response.StatusCode}");
                    return null;
                }
            }
        }

        private async Task HandleSpotifyLogin(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                MessageBox.Show("Spotify login failed. No access token received.");
                return;
            }

            SpotifyAuth.AccessToken = accessToken;

            User user = await GetSpotifyUserProfile(accessToken);
            if (user != null)
            {
                user.AccessToken = accessToken;
                user.RefreshToken = _refreshToken; // Store the refresh token in the user object

                User existingUser = GetUserFromDatabase(user.SpotifyUserId);

                if (existingUser == null)
                {
                    SaveUserToDatabase(user);
                }
                else
                {
                    
                    user.SpotifyUserId = existingUser.SpotifyUserId; 
                    SaveUserToDatabase(user);
                }

                string jwtToken = GenerateJwtToken(user);
                MessageBox.Show("JWT Token generated: " + jwtToken);

               
                Dashboard dashboardForm = new Dashboard(accessToken);
                dashboardForm.Show();
                this.Hide();
            }
        }

        private void signup_Click(object sender, EventArgs e)
        {
            string spotifySignupUrl = "https://www.spotify.com/signup/";
            Process.Start(new ProcessStartInfo
            {
                FileName = spotifySignupUrl,
                UseShellExecute = true 
            });

            
            MessageBox.Show("Complete your Spotify sign-up, then return here to log in.", "Redirecting...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }
    }
}