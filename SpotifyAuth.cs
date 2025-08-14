using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Song_Recommendation_App
{
    public class SpotifyAuth
    {
        private readonly HttpListener _httpListener;
        private readonly string _redirectUri;
        private readonly int _port;
        public static string AccessToken { get; set; }
        public SpotifyAuth(string redirectUri, int port = 5000)
        {
            _redirectUri = redirectUri;
            _port = port;
            _httpListener = new HttpListener();
            _httpListener.Prefixes.Add($"http://localhost:{_port}/callback/");
        }

        public async Task<string> GetAuthorizationCodeAsync()
        {
            _httpListener.Start();

            var context = await _httpListener.GetContextAsync();
            var request = context.Request;

            string authorizationCode = request.QueryString["code"];

  
            var response = context.Response;
            string responseString = "<html><head><meta charset='utf-8'></head><body><h2>Login Successful! You can close this window.</h2></body></html>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            await responseOutput.WriteAsync(buffer, 0, buffer.Length);
            responseOutput.Close();

            _httpListener.Stop();
            return authorizationCode;
        }
        
    }
}
