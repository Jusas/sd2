using System;
using System.Collections.Generic;
using System.Text;

namespace SD2Tools.ReplayTools.Models
{
    public class ReplayFooter
    {
        public ReplayFooterResult Result { get; set; }

        public class ReplayFooterResult
        {
            public string Duration { get; set; }
            public int DurationInt { get; set; }
            public string Victory { get; set; }
            public int VictoryInt { get; set; }
            public string Score { get; set; }
            public int ScoreInt { get; set; }
        }
    }
}
