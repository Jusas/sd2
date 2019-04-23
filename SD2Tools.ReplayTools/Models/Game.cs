using System;
using System.Collections.Generic;
using System.Text;

namespace SD2Tools.ReplayTools.Models
{
    public class Game
    {
        public string Version { get; set; }
        public int VersionInt { get; set; }
        public string ModList { get; set; }
        public string GameMode { get; set; }
        public int GameModeInt { get; set; }
        public string Map { get; set; }
        public string NbMaxPlayer { get; set; }
        public int NbMaxPlayerInt { get; set; }
        public string NbIA { get; set; }
        public int NbIAInt { get; set; }
        public string GameType { get; set; }
        public int GameTypeInt { get; set; }
        public string Private { get; set; }
        public string InitMoney { get; set; }
        public int InitMoneyInt { get; set; }
        public string TimeLimit { get; set; }
        public int TimeLimitInt { get; set; }
        public string ScoreLimit { get; set; }
        public int ScoreLimitInt { get; set; }
        public string ServerName { get; set; }
        public string VictoryCond { get; set; }
        public string IsNetworkMode { get; set; }
        public string IncomeRate { get; set; }
        public int IncomeRateInt { get; set; }
        public string AllowObservers { get; set; }
        public string MapSelection { get; set; }
        public string Seed { get; set; }
        public string UniqueSessionId { get; set; }
        public string InverseSpawnPoints { get; set; }
    }
}
