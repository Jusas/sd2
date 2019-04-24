using System;
using System.Collections.Generic;
using System.Text;

namespace SD2API.Domain
{
    public class Replay
    {
        public virtual int ReplayId { get; set; } // sha-1 of the replay data bytes + name bytes, 10 first chars.
        public string ReplayHashStub { get; set; } // sha-1 of the replay data bytes, 10 first chars.

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }        
        public string BinaryUrl { get; set; }
        public string ReplayRawHeader { get; set; }
        public string ReplayRawFooter { get; set; }
        
        public virtual ReplayHeader ReplayHeader { get; set; }
        public virtual ReplayFooter ReplayFooter { get; set; }

    }

    public class ReplayFooter
    {
        public virtual int ReplayFooterId { get; set; }
        public virtual int ReplayId { get; set; }
        public ReplayFooterResult result { get; set; }

        public virtual Replay Replay { get; set; }

        public class ReplayFooterResult
        {
            public virtual int ReplayFooterResultId { get; set; }
            public virtual int ReplayFooterId { get; set; }

            public string Duration { get; set; }
            public string Victory { get; set; }
            public string Score { get; set; }

            public virtual ReplayFooter ReplayFooter { get; set; }
        }
    }

    public class ReplayHeader
    {
        public ReplayHeader()
        {
            Players = new HashSet<ReplayHeaderPlayer>();
        }

        public virtual int ReplayHeaderId { get; set; }
        public virtual int ReplayId { get; set; }
        public ReplayHeaderGame Game { get; set; }
        public ICollection<ReplayHeaderPlayer> Players { get; set; }

        public virtual Replay Replay { get; set; }

        public class ReplayHeaderGame
        {
            public virtual int ReplayHeaderGameId { get; set; }
            public virtual int ReplayHeaderId { get; set; }

            public string Version { get; set; }
            public string ModList { get; set; }
            public string GameMode { get; set; }
            public string Map { get; set; }
            public string NbMaxPlayer { get; set; }
            public string NbIA { get; set; }
            public string GameType { get; set; }
            public string Private { get; set; }
            public string InitMoney { get; set; }
            public string TimeLimit { get; set; }
            public string ScoreLimit { get; set; }
            public string ServerName { get; set; }
            public string VictoryCond { get; set; }
            public string IsNetworkMode { get; set; }
            public string IncomeRate { get; set; }
            public string AllowObservers { get; set; }
            public string MapSelection { get; set; }
            public string Seed { get; set; }
            public string UniqueSessionId { get; set; }
            public string InverseSpawnPoints { get; set; }

            public virtual ReplayHeader ReplayHeader { get; set; }
        }

        public class ReplayHeaderPlayer
        {
            public virtual int ReplayHeaderPlayerId { get; set; }
            public virtual int ReplayHeaderId { get; set; }

            public string PlayerUserId { get; set; }
            public string PlayerElo { get; set; }
            public string PlayerRank { get; set; }
            public string PlayerLevel { get; set; }
            public string PlayerName { get; set; }
            public string PlayerTeamName { get; set; }
            public string PlayerAvatar { get; set; }
            public string PlayerIALevel { get; set; }
            public string PlayerReady { get; set; }
            public string PlayerDeckContent { get; set; }
            public string PlayerModList { get; set; }
            public string PlayerAlliance { get; set; }
            public string PlayerIsEnteredInLobby { get; set; }
            public string PlayerSkinIndexUsed { get; set; }
            public string PlayerScoreLimit { get; set; }
            public string PlayerIncomeRate { get; set; }

            public virtual ReplayHeader ReplayHeader { get; set; }
        }


    }
}
