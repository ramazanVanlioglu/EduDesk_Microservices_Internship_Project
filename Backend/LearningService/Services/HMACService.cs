using System.Security.Cryptography;
using System.Text;

namespace LearningService.Services
{
    public class HMACService : IHMACService
    {
        private readonly string _secret = "EduDesk_Ozel_HMAC_Key_2026!"; // bu anahtar her hizmette aynı olmalı

        public string CreateSignature(string message)
        {
            var keyBytes = Encoding.UTF8.GetBytes(_secret);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            using var hmac = new HMACSHA256(keyBytes);
            var hashBytes = hmac.ComputeHash(messageBytes);
            
            
            return Convert.ToBase64String(hashBytes);
            
        }

        public bool VerifySignature(string message, string signature)
        {
            var expectedSignature = CreateSignature(message);
            return expectedSignature == signature;
        }

    }
}
