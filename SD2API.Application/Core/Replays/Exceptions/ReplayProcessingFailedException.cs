using System;
using System.Collections.Generic;
using System.Text;

namespace SD2API.Application.Core.Replays.Exceptions
{
    public class ReplayProcessingFailedException : Exception
    {
        public ReplayProcessingFailedException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
