using System;

namespace CrestSharp.Infrastructure
{
    public class CrestException : Exception
    {
        public readonly string ExceptionType;

        public CrestException(string exceptionType, string message) : base($"{exceptionType}: {message}")
        {
            ExceptionType = exceptionType;
        }
    }
}