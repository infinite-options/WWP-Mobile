using System;
using System.Collections.Generic;

namespace WWP.LogInClasses
{
    public class AccountSalt
    {
        public string user_password_algorithm { get; set; }
        public string user_password_salt { get; set; }
        public string user_social_media { get; set; }
    }

    public class AcountSaltCredentials
    {
        public string message { get; set; }
        public int code { get; set; }
        public IList<AccountSalt> result { get; set; }
        public string sql { get; set; }
    }
}
