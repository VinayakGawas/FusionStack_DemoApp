using System;
using System.Collections.Generic;
using System.Text;

namespace FusionStack_DemoApp.Models
{
    public class TokenResult
    {
        public string token { get; set; }
        public DateTime expiration { get; set; }
    }

    public class Root
    {
        public string userId { get; set; }
        public TokenResult tokenResult { get; set; }
        public bool twoFactorRequired { get; set; }
        public string refreshToken { get; set; }
    }
    public class ResponseModel
    {
        public bool success { get; set; }
        public string message { get; set; }
    }

}
