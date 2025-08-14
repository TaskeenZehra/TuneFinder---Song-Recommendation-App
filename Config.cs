using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Song_Recommendation_App
{
    internal class Config
    {
        public static readonly string JwtSecretKey = "g+3k/uaVOl84NhnIUiRjt01hjZKB71X4ffjWXiPgREM=";
        public const string ClientId = "5a2b4100059a47caa9dbcbf59ca75394";
        public const string ClientSecret = "2b83493dcadc4e1abf9521461c658d77";
        public const string RedirectUri = "http://localhost:5000/callback/";
        public const string TokenUrl = "https://accounts.spotify.com/api/token";
        public const string AuthUrl = "https://accounts.spotify.com/authorize";
        public static string ConnectionString = "Server=DESKTOP-84L617B;Database= Music_App; Integrated Security = True; TrustServerCertificate=True";
    }
}
