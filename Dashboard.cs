using Azure.Core;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Song_Recommendation_App
{
    public partial class Dashboard : Form
    {
        private static readonly string[] popularQueries = new[]
{
    "top hits",
    "genre:pop",
    "genre:rock",
    "genre:hip-hop",
    "trending",
    "mood:happy",
    "genre:indie",
    "genre:dance",
    "genre:edm",
    "genre:classical"
};

        private string _accessToken;

        public Dashboard(string accessToken)
        {
            InitializeComponent();
            _accessToken = accessToken; 
        }

        private string _refreshToken; 
        private void logout_Click(object sender, EventArgs e)
        {
            
            ClearUserData();

            MessageBox.Show("You have been logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

            Application.Restart();
        }

        private void ClearUserData()
        {
            
            SpotifyAuth.AccessToken = null; 
            _refreshToken = null; 

            
        }

        private void Contentpnl_Paint(object sender, PaintEventArgs e)
        {
            Contentpnl.BackColor = Color.FromArgb(180, 0, 0, 0); 
        }

        private void LoadContent(UserControl control)
        {
            Contentpnl.Controls.Clear(); 
            control.Dock = DockStyle.Fill; 
            Contentpnl.Controls.Add(control); 
        }

        private void recommended_Click(object sender, EventArgs e)
        {
            var recommendControl = new RecommendedSongsControl(_accessToken);
            LoadContent(recommendControl);
        }

        private void search_Click(object sender, EventArgs e)
        {
           
            Search searchForm = new Search(_accessToken);
            searchForm.Show();
            this.Hide();
        }

        private void playlist_Click(object sender, EventArgs e)
        {

        }

        private void SideBar_Paint(object sender, PaintEventArgs e)
        {
            SideBar.BackColor = Color.FromArgb(180, 0, 0, 0); 
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            await LoadInitialRecommendedSongs();
        }

        private void playlist_Click_1(object sender, EventArgs e)
        {
            MyPlaylistControl myPlaylist = new MyPlaylistControl(_accessToken);

            myPlaylist.Show();

            this.Hide(); 
        }
        private async Task LoadInitialRecommendedSongs()
        {
            Monitoring.StartNewOperation();
            Monitoring.LogInfo("Starting LoadInitialRecommendedSongs...");

            try
            {
                Random rnd = new Random();
                string randomQuery = popularQueries[rnd.Next(popularQueries.Length)];
                Monitoring.LogInfo($"Selected random query: {randomQuery}");
                bool isHealthy = await Monitoring.CheckSpotifyHealth(_accessToken);

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization =
                        new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _accessToken);

                    int limit = 20;
                    string endpoint = $"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(randomQuery)}&type=track&limit={limit}";
                    Monitoring.LogInfo($"Sending GET request to: {endpoint}");

                    HttpResponseMessage response = await client.GetAsync(endpoint);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        string error = $"Spotify API Error: {(int)response.StatusCode} {response.ReasonPhrase}";
                        Monitoring.LogWarning(error);
                        MessageBox.Show($"{error}\n\n{json}");
                        Monitoring.EndOperation("LoadInitialRecommendedSongs");
                        return;
                    }

                    var data = JObject.Parse(json);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Song");
                    dt.Columns.Add("Artist");
                    dt.Columns.Add("Preview URL");

                    foreach (var item in data["tracks"]["items"])
                    {
                        string songName = item["name"]?.ToString();
                        string artistName = item["artists"]?[0]?["name"]?.ToString();
                        string previewUrl = item["preview_url"]?.ToString();

                        dt.Rows.Add(songName, artistName, previewUrl ?? "N/A");
                    }

                    dataGridView1.DataSource = dt;

                    if (dataGridView1.Columns.Contains("Preview URL"))
                    {
                        dataGridView1.Columns["Preview URL"].Visible = false;
                    }

                    Monitoring.LogInfo("Successfully loaded recommended songs.");
                }
            }
            catch (Exception ex)
            {
                Monitoring.LogError("Error loading recommended songs.", ex);
                MessageBox.Show($"Error loading recommended songs: {ex.Message}");
            }
            finally
            {
                Monitoring.EndOperation("LoadInitialRecommendedSongs");
            }

        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            
            Form1 loginForm = new Form1();
            loginForm.Show();

            this.Hide();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
