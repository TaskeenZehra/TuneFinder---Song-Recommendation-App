using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Microsoft.IdentityModel.Tokens;


namespace Song_Recommendation_App
{
    class Program
    {
        [STAThread]
        static void Main()
        {
            Monitoring.InitializeLogging();

            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                Monitoring.LogError("Unhandled exception occurred.", ex);
            }
            finally
            {
                Monitoring.ShutdownLogging();
            }
           
        }
    }

    class JwtHelper
    {
        public static bool ValidateJwtToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            try
            {
                var jwtToken = handler.ReadToken(token) as JwtSecurityToken;
                return jwtToken != null && jwtToken.ValidTo > DateTime.UtcNow;
            }
            catch
            {
                return false;
            }
        }


        private static readonly string _jwtKey = GenerateSecureKey();

        ///  **Generates a secure 256-bit key**
        private static string GenerateSecureKey()
        {
            byte[] keyBytes = new byte[32]; // 256 bits = 32 bytes
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes); //  Always valid 256-bit key
        }

        ///  **Generates a JWT token using a secure SHA256-derived key**
        public static string GenerateJwtToken(string userId)
        {
            var securityKey = new SymmetricSecurityKey(Convert.FromBase64String(_jwtKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "YourApp",
                audience: "YourApp",
                claims: null,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}