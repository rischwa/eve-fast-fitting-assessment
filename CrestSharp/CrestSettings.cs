using System;
using CrestSharp.Caches;
using CrestSharp.Infrastructure;

namespace CrestSharp
{
    public class CrestSettings
    {
        private ICrestCache _cache = new CrestSessionCache();
        private ICrestDocumentLoader _documentLoader = new CrestDocumentLoader();

        public ICrestCache Cache
        {
            get { return _cache; }
            set
            {
                if (value == null)
                {
                    _cache = new CrestNoCache();
                    return;
                }
                _cache = value;
            }
        }

        public string PublicEndpointUri { get; set; } = "https://public-crest.eveonline.com/";
        //public string AuthorizedEndpointUri { get; set; } = "https://public-crest.eveonline.com/";
        public string SsoUrl { get; set; } = "https://login.eveonline.com/";
        public string AuthenticatedCrestBaseUrl { get; set; } = "https://crest-tq.eveonline.com/";

        public string AuthenticationResponseHtml { get; set; } = @"<!DOCTYPE html>
<html><head>
<style>
body{
    text-align: center;
    color: #83BBCF;
    background-color: #020A12;
}
</style>
</head>
<body>
<h3>Successfully authorized, thank you!</h3>
<div>You may now close this window.</div></body></html>";

        public TimeSpan AuthenticationTimeout { get; set; } = TimeSpan.FromSeconds(30);

        public ICrestDocumentLoader DocumentLoader
        {
            get { return _documentLoader; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                _documentLoader = value;
            }
        }
    }
}