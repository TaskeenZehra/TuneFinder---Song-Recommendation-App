using System;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Song_Recommendation_App
{
    public partial class RecommendedSongsControl : UserControl
    {
        private string accessToken;

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
    "genre:classical",
    "genre:romantic",
    "Hindi Songs"


};

        public RecommendedSongsControl(string token)
        {
            InitializeComponent();
            accessToken = token;

        }

        private async void RecommendedSongsControl_Load(object sender, EventArgs e)
        {
            Random rnd = new Random();
            int index = rnd.Next(popularQueries.Length);
            string randomQuery = popularQueries[index];
            await LoadTopSongs(randomQuery);
        }

        private async Task LoadTopSongs(string query)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    int limit = 50;
                    string endpoint = $"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(query)}&type=track&limit={limit}";

                    HttpResponseMessage response = await client.GetAsync(endpoint);
                    string json = await response.Content.ReadAsStringAsync();

                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show($"Error fetching songs: {(int)response.StatusCode} {response.ReasonPhrase}\n\n{json}");
                        return;
                    }

                    JObject data = JObject.Parse(json);

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
                    dataGridView1.Columns["Preview URL"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching top songs: {ex.Message}");
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string previewUrl = dataGridView1.Rows[e.RowIndex].Cells["Preview URL"].Value.ToString();
            if (previewUrl != "N/A")
            {
                System.Diagnostics.Process.Start(previewUrl);
            }
            else
            {
                MessageBox.Show("No preview available for this song.");
            }
        }
    }
}
