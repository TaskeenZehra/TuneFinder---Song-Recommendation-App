using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Azure.Core;
using Newtonsoft.Json.Linq;

namespace Song_Recommendation_App
{
    public partial class MyPlaylistControl : Form
    {
        private string accessToken;

        public MyPlaylistControl(string token)
        {
            InitializeComponent(); 
            accessToken = token;

            Playlistdata.CellBorderStyle = DataGridViewCellBorderStyle.None; 
            Playlistdata.RowHeadersVisible = false;

        }

        private async void MyPlaylistControl_Load(object sender, EventArgs e)
        {
            await LoadPlaylists();
        }

        private async Task LoadPlaylists()
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                MessageBox.Show("Access token is missing. Please log in again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    HttpResponseMessage response = await client.GetAsync("https://api.spotify.com/v1/me/playlists");
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject playlistsData = JObject.Parse(responseBody);

                    var playlists = playlistsData["items"];

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Name");
                    dt.Columns.Add("Tracks");
                    dt.Columns.Add("Playlist ID");

                    foreach (var playlist in playlists)
                    {
                        string name = playlist["name"].ToString();
                        string tracks = playlist["tracks"]["total"].ToString();
                        string id = playlist["id"].ToString();

                        dt.Rows.Add(name, tracks,id);
                    }

                    Playlistdata.DataSource = dt; 
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Error loading playlists: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Playlistdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void backbutton_Click(object sender, EventArgs e)
        {
            Dashboard dashboardForm = new Dashboard(accessToken);
            dashboardForm.Show();

            this.Hide();
        }

        private async void  Playlistdata_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string playlistId = Playlistdata.Rows[e.RowIndex].Cells["Playlist ID"].Value.ToString();

            PlaylistTracksForm tracksForm = new PlaylistTracksForm(accessToken, playlistId);
            tracksForm.Show();
            this.Hide();
        }
    }
}