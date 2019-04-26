using System;
using System.Collections.Generic;
using System.Text;

namespace SD2API.Application.Core.Replays.Exceptions
{
    public class SearchException : Exception
    {
        public SearchException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
