using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SD2Tools.ReplayTools.Tests
{
    public class Utils
    {
        public static Stream ReadReplayFromAssembly(string replayFileName)
        {
            var asm = Assembly.GetAssembly(typeof(Utils));
            var resourceNames = asm.GetManifestResourceNames();
            var resourceName = resourceNames.FirstOrDefault(f =>
                f.EndsWith(replayFileName, StringComparison.InvariantCultureIgnoreCase));
            if (resourceName != null)
                return asm.GetManifestResourceStream(resourceName);

            throw new Exception("Replay file not found from assembly");
        }
    }
}
