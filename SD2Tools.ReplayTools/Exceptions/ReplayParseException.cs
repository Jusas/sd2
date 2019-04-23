using System;
using System.Collections.Generic;
using System.Text;

namespace SD2Tools.ReplayTools.Exceptions
{
    public class ReplayParseException : Exception
    {
        public ReplayParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
