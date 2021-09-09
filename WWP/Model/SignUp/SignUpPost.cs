using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WWP.Model.Login.Constants;
using Xamarin.Forms;

namespace WWP.Model.SignUp
{

    // object to send to database when user attempts to sign up 
    // link: https://uavi7wugua.execute-api.us-west-1.amazonaws.com/dev/api/v2/signup
    public class SignUpPost
    {
        public string role { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string password { get; set; }
        public string social_id { get; set; }
        public string social { get; set; }
        public string mobile_access_token { get; set; }
        public string mobile_refresh_token { get; set; }
        public string user_access_token { get; set; }
        public string user_refresh_token { get; set; }

        public SignUpPost(string userType, string accountType)
        {
            role = userType;

            if(accountType == "DIRECT")
            {
                social_id = "NULL";
                social = "FALSE";
                mobile_access_token = "FALSE";
                mobile_refresh_token = "FALSE";
                user_access_token = "FALSE";
                user_refresh_token = "FALSE";
            }
            else if (accountType == "SOCIAL")
            {
                password = "NULL";
                user_access_token = "FALSE";
                user_refresh_token = "FALSE";
            }
        }

        public static async Task<string> signUpUser(SignUpPost newUser)
        {
            try
            {
                var userID = "";
                var client = new HttpClient();
                var serializedObject = JsonConvert.SerializeObject(newUser);
                var content = new StringContent(serializedObject, Encoding.UTF8, "application/json");
                var endpointCall = await client.PostAsync(Constant.signUpEndpoint, content);

                Debug.WriteLine("USER ROLE: " + newUser.role);

                if (endpointCall.IsSuccessStatusCode)
                {
                    var endpointContentString = await endpointCall.Content.ReadAsStringAsync();
                    var parsedData = JsonConvert.DeserializeObject<SignUpResponse>(endpointContentString);
                    if (parsedData.code.ToString() != Constant.EmailAlreadyExist)
                    {
                        userID = parsedData.result.user_uid;
                    }
                    else
                    {
                        userID = "USER ALREADY EXIST";
                    }
                }

                return userID;
            }
            catch (Exception issueSignUpUser)
            {
                Generic gen = new Generic();
                gen.parseException(issueSignUpUser.ToString());
                return "";
            }
        }

        public static bool ValidateSignUpInfo(Entry email1, Entry email2, Entry password1, Entry password2)
        {
            bool result = false;
            if (!(
                   String.IsNullOrEmpty(email1.Text)
                || String.IsNullOrEmpty(email2.Text)
                || String.IsNullOrEmpty(password1.Text)
                || String.IsNullOrEmpty(password2.Text)
                ))
            {
                result = true;
            }
            return result;
        }

        public static bool ValidateEmails(Entry email1, Entry email2)
        {
            bool result = false;

            if (!(String.IsNullOrEmpty(email1.Text) || String.IsNullOrEmpty(email2.Text)))
            {
                if(email1.Text == email2.Text)
                {
                    result = true;
                }
            }

            return result;

        }

        public static bool ValidatePasswords(Entry password1, Entry password2)
        {
            bool result = false;

            if (!(String.IsNullOrEmpty(password1.Text) || String.IsNullOrEmpty(password2.Text)))
            {
                if (password1.Text == password2.Text)
                {
                    result = true;
                }
            }

            return result;
        }


    }
}