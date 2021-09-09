using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace WWP.Model.SignUp
{
    public class SignUpResponse
    {
        public string message { get; set; }
        public int code { get; set; }
        public SignUpResult result { get; set; }
        public string sql { get; set; }
    }

    public class SignUpResult
    {
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string user_uid { get; set; }
        public string user_access_token { get; set; }
        public string user_refresh_token { get; set; }
        public string mobile_access_token { get; set; }
        public string mobile_refresh_token { get; set; }
        public string user_social_media_id { get; set; }
    }
}