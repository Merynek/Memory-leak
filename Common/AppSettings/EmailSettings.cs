﻿namespace Common.AppSettings
{
    public class EmailSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public string BrevoApiKey { get; set; }
    }
}
