using System;
namespace WWP.Constants
{
    // Constants file should not be included in git push
    public class Constant
    {
        // FACEBOOK CONSTANTS
        public static string FacebookScope = "email";
        public static string FacebookAuthorizeUrl = "https://www.facebook.com/dialog/oauth/";
        public static string FacebookAccessTokenUrl = "https://www.facebook.com/connect/login_success.html";
        public static string FacebookUserInfoUrl = "https://graph.facebook.com/me?fields=email,name,picture&access_token=";

        // FACEBOOK ID WWP
        public static string FacebookAndroidClientID = "813401236080861";
        public static string FacebookiOSClientID = "813401236080861";

        public static string FacebookiOSRedirectUrl = "https://www.facebook.com/connect/login_success.html:/oauth2redirect";
        public static string FacebookAndroidRedirectUrl = "https://www.facebook.com/connect/login_success.html";

        // GOOGLE CONSTANTS
        public static string GoogleScope = "https://www.googleapis.com/auth/userinfo.profile https://www.googleapis.com/auth/userinfo.email";
        public static string GoogleAuthorizeUrl = "https://accounts.google.com/o/oauth2/v2/auth";
        public static string GoogleAccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string GoogleUserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        //updated 
        public static string GoogleiOSClientID = "799054437694-btjpb6hbp3r71psrvtfdr5qqb3hl5h67.apps.googleusercontent.com";
        public static string GoogleAndroidClientID = "799054437694-5qaill0n61e2j3h1ui5hl84uih16qadg.apps.googleusercontent.com";

        // WWP
        //public static string GoogleiOSClientID = "736355098040-dfnf83qu5t7ocibh2iajqpiddaoe0qq9.apps.googleusercontent.com";
        //public static string GoogleAndroidClientID = "736355098040-q7nr6ftln8ffp55jl4os3l3auvnq8k23.apps.googleusercontent.com";
        public static string BaseUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev";

        //updated
        public static string GoogleRedirectUrliOS = "com.googleusercontent.apps.799054437694-btjpb6hbp3r71psrvtfdr5qqb3hl5h67:/oauth2redirect";
        public static string GoogleRedirectUrlAndroid = "com.googleusercontent.apps.799054437694-5qaill0n61e2j3h1ui5hl84uih16qadg:/oauth2redirect";


        // WWP
        //public static string GoogleRedirectUrliOS = "com.googleusercontent.apps.736355098040-dfnf83qu5t7ocibh2iajqpiddaoe0qq9:/oauth2redirect";
        //public static string GoogleRedirectUrlAndroid = "com.googleusercontent.apps.736355098040-q7nr6ftln8ffp55jl4os3l3auvnq8k23:/oauth2redirect";

        // ENDPOINTS M4ME
        //public static string AccountSaltUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/accountsalt";
        //public static string LogInUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/login";
        //public static string SignUpUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/createAccount";
        //public static string UpdateTokensUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/token_fetch_update/update_mobile";
        //public static string AppleEmailUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/AppleEmail";
        //public static string StripeModeUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/Stripe_Payment_key_checker";
        //public static string PayPalModeUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/Paypal_Payment_key_checker";
        //public static string GuidUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/update_guid_notification/customer,add";
        public static string DeletePlanUrl = "https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/cancel_purchase";


        //WWP ENDPOINTS
        public static string UpdateTokensUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev/api/v2/token_fetch_update/update_mobile";
        public static string AppleEmailUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev/api/v2/AppleEmail";
        public static string StripeModeUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev/api/v2/Stripe_Payment_key_checker";
        public static string PayPalModeUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev/api/v2/Paypal_Payment_key_checker";
        public static string GuidUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev/api/v2/update_guid_notification/customer,add";
        public static string AccountSaltUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev/api/v2/accountsalt";
        public static string LogInUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev/api/v2/login";
        public static string SignUpUrl = "https://c1zwsl05s5.execute-api.us-west-1.amazonaws.com/dev/api/v2/createAccount";

        //https://ht56vci4v9.execute-api.us-west-1.amazonaws.com/dev/api/v2/
        // RDS CODES
        public static string EmailNotFound = "404";
        public static string ErrorPlatform = "411";
        public static string ErrorUserDirectLogIn = "406";
        public static string UseSocialMediaLogin = "401";
        public static string AutheticatedSuccesful = "200";

        // PLATFORM
        public static string Google = "GOOGLE";
        public static string Facebook = "FACEBOOK";

        // EXTENDED TIME
        public static double days = 14;

        // PAYPAL CREDENTIALS - WWP
        public static string TestClientId = "ATnaX-KW9jaomOfSgQqmVbQNt2s8IsnhikKOIiMw47YzB--uWlLZgWoPuxoRuHPqhgZFXnmrGCu4jmVr";
        public static string TestSecret = "EKHC9S3DloGNubSl18XDFmAt7ca9Vvj3TRa-r3hTdA2SCW4yvNUfa9ZLR-fxGxowrC5Ae0_XBFWkBXOd";

        public static string LiveClientId = "AXhkFKdvsXMoQ5gHgwBM03cKUumitEDI779oyWp5VidFf9jSbW8ls5yZxVxebaA1JVdRhfEzwRYLg3P1";
        public static string LiveSecret = "ENvZD6pgioAjT67Ggp4wt-ta0XA7yuOLLHYy28zAMwNua4-3QMJ-B9dcLXbEA5v9fp1fA27xa_4KlqfY";

        // STRIPE CREDENTIALS - WWP
        public static string TestPK = "pk_test_51HyqrgLMju5RPMEv5ai8f5nU87HWQFNXOZmLTWLIrqlNFMPjrboGfQsj4FDUvaHRAhxyRBQrfhmXC3kMnxEYRiKO00m4W3jj5a";
        public static string TestSK = "sk_test_51HyqrgLMju5RPMEvowxoZHOI9LjFSxI9X3KPsOM7KVA4pxtJqlEwEkjLJ3GCL56xpIQuVImkSwJQ5TqpGkl299bo00yD1lTRNK";

        public static string LivePK = "pk_live_51HyqrgLMju5RPMEvR4n6Bonvf1k3Lmi69HOk95j1bt7BKTEbiSwFh7LELos0rAc5LwBC2MrD0MK1QRHTdpu5uT3I00KhYZVKcE";
        public static string LiveSK = "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";

        public static string Contry = "US";

        //azure notification hub constants
        public static string NotificationChannelName { get; set; } = "XamarinNotifyChannel";
        public static string NotificationHubName { get; set; } = "< Insert your Azure Notification Hub name >";
        public static string ListenConnectionString { get; set; } = "< Insert your DefaultListenSharedAccessSignature >";
        public static string DebugTag { get; set; } = "XamarinNotify";
        public static string[] SubscriptionTags { get; set; } = { "default" };
        public static string FCMTemplateBody { get; set; } = "{\"data\":{\"message\":\"$(messageParam)\"}}";
        public static string APNTemplateBody { get; set; } = "{\"aps\":{\"alert\":\"$(messageParam)\"}}";

        // Google Places API
        public const string GooglePlacesApiKey = "AIzaSyBGgoTWGX2mt4Sp8BDZZntpgxW8Cq7Qq90";
    }
}
