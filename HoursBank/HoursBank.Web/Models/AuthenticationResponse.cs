﻿using System;

namespace HoursBank.Web.Models
{
    public class AuthenticationResponse
    {
        public bool Authenticated { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expiration { get; set; }
        public string AccessToken { get; set; }
        public string UserEmail { get; set; }
        public string Message { get; set; }
    }
}
