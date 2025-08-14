using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace jwtkey
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string key =GenerateSecureKey();
            Console.WriteLine(key);
        }
        private static string GenerateSecureKey()
        {
            byte[] keyBytes = new byte[32]; // 256 bits = 32 bytes
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(keyBytes);
            }
            return Convert.ToBase64String(keyBytes); // ✅ Always valid 256-bit key
        }

    }
}
