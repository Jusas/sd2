using System;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace SD2Tools.ReplayTools.Tests
{
    public class ReplayParseTests
    {
        [Fact]
        public async Task Should_parse_valid_replay_without_exceptions()
        {
            // Arrange
            var replayStream = Utils.ReadReplayFromAssembly("minor_axis_defeat.rpl3");

            // Act
            var replay = await Replay.FromStream(replayStream);

            // Assert
            Assert.Single(replay.ReplayHeader.Players);
            replay.ReplayHeader.Game.VersionInt.Should().Be(60104754);
            replay.ReplayHeader.Game.TimeLimit.Should().Be("0");
            replay.ReplayHeader.Game.Map.Should().Be("_2x1_Proto_levelBuild_Orsha_N_LD_1v1");

            replay.ReplayHeader.Players.First().PlayerUserId.Should().Be("28122");
            replay.ReplayHeader.Players.First().PlayerLevelInt.Should().Be(5);
        }
    }
}
