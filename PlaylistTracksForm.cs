using System;
using System.Data;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Song_Recommendation_App
{
    public partial class PlaylistTracksForm : Form
    {
        private string accessToken;
        private string playlistId;

        public PlaylistTracksForm(string token, string id)
        {
            InitializeComponent();
            accessToken = token;
            playlistId = id;

            Playlistdata.CellContentClick += Playlistdata_CellContentClick;
            this.Load += PlaylistTracksForm_Load;
        }

        private async void PlaylistTracksForm_Load(object sender, EventArgs e)
        {
            await LoadTracks();
        }

        private async Task LoadTracks()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    string url = $"https://api.spotify.com/v1/playlists/{playlistId}/tracks";
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    JObject json = JObject.Parse(responseBody);

                    DataTable dt = new DataTable();
                    dt.Columns.Add("Track Name");
                    dt.Columns.Add("Artist");
                    dt.Columns.Add("Preview URL");
                    dt.Columns.Add("Type");

                    foreach (var item in json["items"])
                    {
                        string trackName = item["track"]?["name"]?.ToString() ?? "Unknown";
                        string artistName = item["track"]?["artists"]?[0]?["name"]?.ToString() ?? "Unknown";
                        string previewUrl = item["track"]?["preview_url"]?.ToString();
                        string type = item["track"]?["type"]?.ToString() ?? "Unknown";

                        dt.Rows.Add(trackName, artistName, previewUrl, type);
                    }

                    Playlistdata.DataSource = dt;


                    if (Playlistdata.Columns.Contains("Preview URL"))
                    {
                        Playlistdata.Columns["Preview URL"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load tracks: {ex.Message}");
            }
        }

        private void Playlistdata_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string previewUrl = Playlistdata.Rows[e.RowIndex].Cells["Preview URL"].Value?.ToString();

                if (!string.IsNullOrEmpty(previewUrl))
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(previewUrl) { UseShellExecute = true });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to play the song: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("No preview available for this song.");
                }
            }
        }

        private void backbutton_Click(object sender, EventArgs e)
        {

            MyPlaylistControl dashboardForm = new MyPlaylistControl(accessToken);
            dashboardForm.Show();
            this.Hide();
        }

        private void PlaylistTracksForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}
