using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Song_Recommendation_App
{
    public partial class Search : Form
    {
        private string _accessToken;
        private const string SpotifyApiUrl = "https://api.spotify.com/v1";
        private string _playlistId; 
        public Search()
        {
            InitializeComponent();
            _accessToken = string.Empty; 
            _playlistId = string.Empty;
        }

        public Search(string accessToken) : this()
        {
            _accessToken = accessToken;
        }

        private void Search_Load(object sender, EventArgs e)
        {
           
            this.Text = "Search Music - Spotify";
            this.StartPosition = FormStartPosition.CenterScreen;
            Searchdata.CellBorderStyle = DataGridViewCellBorderStyle.None; 
            Searchdata.RowHeadersVisible = false; 
            Searchdata.AllowUserToAddRows = false;
            InitializeDataGridView(); 
            Searchdata.BorderStyle = BorderStyle.None;
        }

        private void InitializeDataGridView()
        {
            
            Searchdata.Columns.Clear(); 
            Searchdata.Columns.Add("Name", "Name");
            Searchdata.Columns.Add("Artist/Album", "Artist/Album");
            Searchdata.Columns.Add("Type", "Type");
            Searchdata.Columns.Add("Id", "Id");
            DataGridViewButtonColumn addToPlaylistButtonColumn = new DataGridViewButtonColumn();
            addToPlaylistButtonColumn.Name = "AddToPlaylist";
            addToPlaylistButtonColumn.HeaderText = "Add to Playlist"; 
            addToPlaylistButtonColumn.Text = "Add to Playlist";
            addToPlaylistButtonColumn.UseColumnTextForButtonValue = true; 
            Searchdata.Columns.Add(addToPlaylistButtonColumn);
            Searchdata.Columns["Id"].Visible = false; 
            Searchdata.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Searchdata.AutoGenerateColumns = false; 
        }

        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void searchbtn_Click(object sender, EventArgs e)
        {
           
            string query = searchBox.Text.Trim();
            if (!string.IsNullOrEmpty(query))
            {
                SearchSpotify(query);
            }
            else
            {
                MessageBox.Show("Please enter a song, artist, or album name to search.", "Empty Search", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Searchdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == Searchdata.Columns["AddToPlaylist"].Index) 
            {
                
                string trackId = Searchdata.Rows[e.RowIndex].Cells["Id"].Value?.ToString(); 
                if (!string.IsNullOrEmpty(trackId))
                {
                    AddToPlaylist(trackId);
                }
                else
                {
                    MessageBox.Show("Track ID is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void Searchdata_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) 
            {
                
                string trackId = Searchdata.Rows[e.RowIndex].Cells["Id"].Value?.ToString(); 
                if (!string.IsNullOrEmpty(trackId))
                {
                    PlaySong(trackId);
                }
                else
                {
                    MessageBox.Show("Track ID is invalid.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private async void PlaySong(string trackId)
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                MessageBox.Show("Please log in to Spotify first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                    
                    var requestBody = new
                    {
                        uris = new[] { $"spotify:track:{trackId}" }
                    };

                    string apiUrl = "https://api.spotify.com/v1/me/player/play";
                    var jsonContent = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PutAsync(apiUrl, content);
                    response.EnsureSuccessStatusCode();

                    MessageBox.Show("Playing song!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Spotify API Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Spotify API Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while playing song: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }

        private async void SearchSpotify(string query)
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                MessageBox.Show("Please log in to Spotify first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                    string apiUrl = $"{SpotifyApiUrl}/search?q={Uri.EscapeDataString(query)}&type=track,artist,album&limit=20";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string jsonResult = await response.Content.ReadAsStringAsync();

                    dynamic json = JsonConvert.DeserializeObject(jsonResult);

                  
                    Searchdata.Rows.Clear();

                    if (json?.tracks?.items != null)
                    {
                        foreach (var item in json.tracks.items)
                        {
                            string artists = string.Join(", ", ((JArray)item.artists).Select(a => (string)a["name"]));
                            Searchdata.Rows.Add(item.name.ToString(), artists, "Track", item.id.ToString());
                        }
                    }

                    if (json?.artists?.items != null)
                    {
                        foreach (var item in json.artists.items)
                        {
                            Searchdata.Rows.Add(item.name.ToString(), "", "Artist", item.id.ToString());
                        }
                    }

                    if (json?.albums?.items != null)
                    {
                        foreach (var item in json.albums.items)
                        {
                            string artists = string.Join(", ", ((JArray)item.artists).Select(a => (string)a["name"]));
                            Searchdata.Rows.Add(item.name.ToString(), artists, "Album", item.id.ToString());
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Spotify API Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Spotify API Error: {ex.Message}");
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing Spotify response: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"JSON Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while searching Spotify: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"General Error: {ex.Message}");
            }
        }

        private void ProcessSearchResults(dynamic json, DataTable dt)
        {
            try
            {
                dt.Rows.Clear(); 
                if (json?.tracks?.items != null)
                {
                    foreach (var item in json.tracks.items)
                    {
                        string artists = string.Join(", ", ((JArray)item.artists).Select(a => (string)a["name"]));
                        dt.Rows.Add(item.name, artists, "Track", item.id);
                    }
                }

                if (json?.artists?.items != null)
                {
                    foreach (var item in json.artists.items)
                    {
                        dt.Rows.Add(item.name, "", "Artist", item.id);
                    }
                }

                if (json?.albums?.items != null)
                {
                    foreach (var item in json.albums.items)
                    {
                        string artists = string.Join(", ", ((JArray)item.artists).Select(a => (string)a["name"]));
                        dt.Rows.Add(item.name, artists, "Album", item.id);
                    }
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing search results: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Error processing search results: {ex.Message}");
            }
        }
        private async Task<List<(string name, string id)>> GetUserPlaylists()
        {
            var playlists = new List<(string name, string id)>();

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    string url = $"{SpotifyApiUrl}/me/playlists?limit=10";

                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    dynamic data = JsonConvert.DeserializeObject(json);

                    foreach (var item in data.items)
                    {
                        playlists.Add(((string)item.name, (string)item.id));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error fetching playlists: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return playlists;
        }

        private async Task<string> AskUserToSelectPlaylist()
        {
            var playlists = await GetUserPlaylists();
            if (playlists.Count == 0)
            {
                MessageBox.Show("No playlists found. Creating a new playlist.", "No Playlist", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return await CreateNewPlaylist("My Playlist");
            }

            using (var form = new Form())
            {
                form.Text = "Select Playlist";
                var combo = new ComboBox { DataSource = playlists, DisplayMember = "name", ValueMember = "id", Dock = DockStyle.Top };
                var ok = new Button { Text = "OK", DialogResult = DialogResult.OK, Dock = DockStyle.Bottom };

                form.Controls.Add(combo);
                form.Controls.Add(ok);
                form.AcceptButton = ok;

                if (form.ShowDialog() == DialogResult.OK)
                {
                    return ((ValueTuple<string, string>)combo.SelectedItem).Item2;
                }
                else
                {
                    return null;
                }
            }
        }

        private async void AddToPlaylist(string trackId)
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                MessageBox.Show("Please log in to Spotify first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrEmpty(_playlistId))
                {
                    var playlists = await GetUserPlaylists();

                    if (playlists.Count == 0)
                    {
                        MessageBox.Show("No playlists found in your account.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string playlistNames = string.Join("\n", playlists.Select((p, i) => $"{i + 1}. {p.name}"));
                    string input = Microsoft.VisualBasic.Interaction.InputBox($"Select a playlist by number:\n{playlistNames}", "Choose Playlist", "1");

                    if (int.TryParse(input, out int selectedIndex) && selectedIndex >= 1 && selectedIndex <= playlists.Count)
                    {
                        _playlistId = playlists[selectedIndex - 1].id;
                    }
                    else
                    {
                        MessageBox.Show("Invalid selection. Operation cancelled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                    var requestBody = new
                    {
                        uris = new[] { $"spotify:track:{trackId}" }
                    };

                    string apiUrl = $"https://api.spotify.com/v1/playlists/{_playlistId}/tracks";
                    var jsonContent = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    string resultJson = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("✅ Track added to playlist successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"❌ Failed to add track. Spotify returned:\n{resultJson}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error occurred:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task<string> CreateNewPlaylist(string name)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);

                // Get current user ID
                var userResponse = await client.GetAsync($"{SpotifyApiUrl}/me");
                var userContent = await userResponse.Content.ReadAsStringAsync();
                dynamic userJson = JsonConvert.DeserializeObject(userContent);
                string userId = userJson.id;

                string url = $"{SpotifyApiUrl}/users/{userId}/playlists";
                var payload = new { name = name, description = "Playlist created from app", @public = false };
                var content = new StringContent(JsonConvert.SerializeObject(payload), System.Text.Encoding.UTF8, "application/json");

                var response = await client.PostAsync(url, content);
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                dynamic playlist = JsonConvert.DeserializeObject(result);
                return playlist.id;
            }
        }

     
        private async Task<string> GetCurrentUserId()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                MessageBox.Show("Please log in to Spotify first!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);
                    string apiUrl = "https://api.spotify.com/v1/me";
                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    response.EnsureSuccessStatusCode();
                    string jsonResult = await response.Content.ReadAsStringAsync();
                    dynamic json = JsonConvert.DeserializeObject(jsonResult);
                    string userId = json.id;
                    return userId;
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Spotify API Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"Spotify API Error: {ex.Message}");
                return null;
            }
            catch (JsonException ex)
            {
                MessageBox.Show($"Error parsing Spotify response: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"JSON Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while getting user profile: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Console.WriteLine($"General Error: {ex.Message}");
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dashboard dashboardForm = new Dashboard(_accessToken);
            dashboardForm.Show();

            this.Hide();
        }
    }
}

