using System;

namespace MyStore.Infrastructure.Jwt
{
    public class JsonWebToken
    {
        public string AccessToken { get; set; }
//        public string RefreshToken { get; set; }
        public long Expires { get; set; }
        public Guid UserId { get; set; }
        public string Role { get; set; }
    }
}