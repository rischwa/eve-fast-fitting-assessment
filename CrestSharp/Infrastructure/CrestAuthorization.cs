using System;

namespace CrestSharp.Infrastructure
{
    public class CrestAuthorization
    {
        public string AccessToken { get; private set; }

        public DateTime EndOfValidity { get; private set; }

        public string TokenType { get; private set; }

        public string RefreshToken { get; private set; }

        public CrestAuthorization(string accessToken, DateTime endOfValidity, string tokenType, string refreshToken)
        {
            AccessToken = accessToken;
            EndOfValidity = endOfValidity;
            TokenType = tokenType;
            RefreshToken = refreshToken;
        }
    }
}