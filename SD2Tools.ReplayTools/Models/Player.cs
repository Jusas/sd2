using System;
using System.Collections.Generic;
using System.Text;

namespace SD2Tools.ReplayTools.Models
{
    public class Player
    {
        public string PlayerUserId { get; set; }
        public string PlayerElo { get; set; }
        public int PlayerEloInt { get; set; }
        public string PlayerRank { get; set; }
        public int PlayerRankInt { get; set; }
        public string PlayerLevel { get; set; }
        public int PlayerLevelInt { get; set; }
        public string PlayerName { get; set; }
        public string PlayerTeamName { get; set; }
        public string PlayerAvatar { get; set; }
        public string PlayerIALevel { get; set; }
        public int PlayerIALevelInt { get; set; }
        public string PlayerReady { get; set; }
        public string PlayerDeckContent { get; set; }
        public string PlayerModList { get; set; }
        public string PlayerAlliance { get; set; }
        public int PlayerAllianceInt { get; set; }
        public string PlayerIsEnteredInLobby { get; set; }
        public string PlayerSkinIndexUsed { get; set; }
        public string PlayerScoreLimit { get; set; }
        public string PlayerIncomeRate { get; set; }
    }
}
