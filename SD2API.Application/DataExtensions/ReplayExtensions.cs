using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using SD2Tools.ReplayTools.Models;
using ReplayHeader = SD2API.Domain.ReplayHeader;

namespace SD2API.Application.DataExtensions
{
    public static class ReplayExtensions
    {

        public static ReplayHeader.ReplayHeaderGame ToDomainReplayHeaderGame(this Game game)
        {
            var parsedGameProps = game.GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var replayHeaderGameProps = typeof(ReplayHeader.ReplayHeaderGame)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var newGameInstance = new ReplayHeader.ReplayHeaderGame();
            foreach (var prop in parsedGameProps)
            {
                PropertyInfo replayHeaderGameProp;
                if ((replayHeaderGameProp = replayHeaderGameProps.FirstOrDefault(x => x.Name == prop.Name)) != null)
                {
                    replayHeaderGameProp.SetValue(newGameInstance, prop.GetValue(game));
                }
            }

            return newGameInstance;
        }

        public static ICollection<ReplayHeader.ReplayHeaderPlayer> ToDomainReplayPlayerList(this List<Player> players)
        {
            var parsedPlayerProps = typeof(Player)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var replayHeaderPlayerProps = typeof(ReplayHeader.ReplayHeaderPlayer)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);

            var newPlayerList = new HashSet<ReplayHeader.ReplayHeaderPlayer>();
            foreach (var player in players)
            {
                var newPlayerInstance = new ReplayHeader.ReplayHeaderPlayer();
                foreach (var prop in parsedPlayerProps)
                {
                    PropertyInfo replayHeaderPlayerProp;
                    if ((replayHeaderPlayerProp = replayHeaderPlayerProps.FirstOrDefault(x => x.Name == prop.Name)) != null)
                    {
                        replayHeaderPlayerProp.SetValue(newPlayerInstance, prop.GetValue(player));
                    }
                }

                newPlayerList.Add(newPlayerInstance);
            }

            return newPlayerList;
        }

    }
}
