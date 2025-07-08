using System;
using System.Collections.Generic;
using System.Configuration;

    public static class EmailServiceCredentials
    {
        public static string EmailSMTPUrl { get; private set; }
        public static string PortNumber { get; private set; }
        public static string EmailSMTPPasswordHash { get; private set; }
        public static string EmailFromAddress { get; private set; }
        public static string EmailFromName { get; private set; }
        public static string EmailAppName { get; private set; }

        public static void SetCredentials(string emailSMTPUrl, string portNumber, string emailSMTPPasswordHash, string emailFromAddress, string emailFromName, string emailAppName)
        {
            EmailSMTPUrl = emailSMTPUrl;
            PortNumber = portNumber;
            EmailSMTPPasswordHash = emailSMTPPasswordHash;
            EmailFromAddress = emailFromAddress;
            EmailFromName = emailFromName;
            EmailAppName = emailAppName;

            // System.Diagnostics.Debug.WriteLine("SMTP URL: " + EmailSMTPUrl);
            // System.Diagnostics.Debug.WriteLine("port number: " + PortNumber);
            // System.Diagnostics.Debug.WriteLine("from address: " + EmailFromAddress);
            // System.Diagnostics.Debug.WriteLine("password: " + EmailSMTPPasswordHash);
    }

        // Call from global application
        public static void PopulateEmailCredentialsFromAppConfig()
        {
            
            string emailSMTPURL = ConfigurationManager.AppSettings["emailSMTPURL"].ToString();
            string portNumber = ConfigurationManager.AppSettings["portNumber"].ToString();
            string emailSMTPPasswordHash = ConfigurationManager.AppSettings["emailSMTPPasswordHash"].ToString();
            string emailFromAddress = ConfigurationManager.AppSettings["emailFromAddress"].ToString();
            string emailFromName = ConfigurationManager.AppSettings["emailFromName"].ToString();
            string emailAppName = ConfigurationManager.AppSettings["emailAppName"].ToString();
            if (string.IsNullOrEmpty(emailSMTPURL) || string.IsNullOrEmpty(portNumber) ||
                string.IsNullOrEmpty(emailSMTPPasswordHash) || string.IsNullOrEmpty(emailFromAddress) ||
                string.IsNullOrEmpty(emailFromName) || string.IsNullOrEmpty(emailAppName))
            {
                throw new Exception("missing required email configuration settings");
            }
        SetCredentials(emailSMTPURL, portNumber, emailSMTPPasswordHash, emailFromAddress, emailFromName, emailAppName);
        }
}