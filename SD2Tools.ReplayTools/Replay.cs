using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SD2Tools.ReplayTools.Exceptions;
using SD2Tools.ReplayTools.Models;
using SD2Tools.ReplayTools.Utils;

namespace SD2Tools.ReplayTools
{
    public class Replay
    {
        private bool _ownsStream = true;
        private Stream _dataStream;

        public ReplayHeader ReplayHeader { get; private set; }
        public string ReplayHeaderRaw { get; private set; }
        public ReplayFooter ReplayFooter { get; private set; }
        public string ReplayFooterRaw { get; private set; }

        public static async Task<Replay> FromFile(string filename)
        {
            Replay r = new Replay(filename);
            try
            {
                await r.ValidateAndReadHeader();
                await r.ValidateAndReadFooter();
            }
            catch (Exception e)
            {
                throw new ReplayParseException("Header/footer validation failed", e);
            }
            return r;
        }

        /// <summary>
        /// Initializes a replay from a stream.
        /// Note: the creator of the stream is also responsible for disposing it.
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static async Task<Replay> FromStream(Stream stream)
        {
            Replay r = new Replay(stream);
            try
            {
                await r.ValidateAndReadHeader();
                await r.ValidateAndReadFooter();
            }
            catch (Exception e)
            {
                throw new ReplayParseException("Header/footer validation failed", e);
            }

            return r;
        }

        private Replay(Stream dataStream)
        {
            _ownsStream = false;
            _dataStream = dataStream;
        }

        public Replay(string filename)
        {
            _dataStream = File.Open(filename, FileMode.Open);
        }

        private async Task ValidateAndReadHeader() // todo validate validation...
        {
            if (_dataStream == null)
                throw new InvalidDataException("Data stream was null!");

            byte[] buf = new byte[4];
            int bytesRead = await _dataStream.ReadAsync(buf, 0, 4);
            if (bytesRead != 4 || Encoding.ASCII.GetString(buf) != "ESAV")
                throw new Exception("Invalid replay file header (ESAV missing)!");


            int replayLengthOffset = 4 /*ESAV*/ + 4 /*4 bytes after rply*/;
            bool foundRply = false;
            while (await _dataStream.ReadAsync(buf, 0, 4) == 4)
            {
                replayLengthOffset += 4;
                if (Encoding.ASCII.GetString(buf) == "rply")
                {
                    foundRply = true;
                    break;
                }
            }

            if (!foundRply)
                throw new Exception("Invalid replay file header (rply missing)!");

            var headerDataPos = replayLengthOffset + 8;

            _dataStream.Seek(replayLengthOffset, SeekOrigin.Begin);
            bytesRead = await _dataStream.ReadAsync(buf, 0, 4);
            if (bytesRead != 4)
                throw new Exception("Unexpected number of bytes read while reading header!");

            if (BitConverter.IsLittleEndian)
                buf = buf.Reverse().ToArray();

            int headerDataLength = BitConverter.ToInt32(buf, 0);
            if (headerDataLength <= 0 || headerDataLength > 50000)
                throw new Exception("Unexpected header data length read, expected a value between 0 and 50 000!");

            _dataStream.Seek(headerDataPos, 0);
            buf = new byte[headerDataLength];
            bytesRead = await _dataStream.ReadAsync(buf, 0, headerDataLength);
            if (bytesRead != headerDataLength)
                throw new Exception(string.Format("Could not read the full header data section: expected {0} bytes, read {1} bytes.",
                    headerDataLength, bytesRead));

            string headerJson = Encoding.UTF8.GetString(buf);

            _dataStream.Seek(0, 0);

            if (_ownsStream)
                _dataStream.Dispose();

            ReplayHeaderRaw = headerJson;
            ReplayHeader = JsonConvert.DeserializeObject<ReplayHeader>(headerJson);            
            ValidateReplayHeaderData(ReplayHeader);
            AssignNumericHeaderProperties();
        }

        private void AssignNumericHeaderProperties()
        {
            PropertyUtils.PopulateIntProperties(ReplayHeader.Game);

            foreach (var player in ReplayHeader.Players)
            {
                PropertyUtils.PopulateIntProperties(player);
            }
        }

        private void AssignNumericFooterProperties()
        {
            PropertyUtils.PopulateIntProperties(ReplayFooter.Result);
        }

        private async Task ValidateAndReadFooter()
        {
            if (_dataStream == null)
                throw new InvalidDataException("Data stream was null!");

            _dataStream.Seek(0, SeekOrigin.End);
            long index = 0;
            long fromEnd = 0;
            byte[] buf = new byte[4];
            long footerMaxExpectedLen = 512;

            while (index == 0)
            {
                fromEnd -= 1;
                _dataStream.Seek(fromEnd, SeekOrigin.End);
                await _dataStream.ReadAsync(buf, 0, 4);
                var bytesAsString = Encoding.UTF8.GetString(buf);
                if (bytesAsString == "rslt")
                    index = _dataStream.Length - Math.Abs(fromEnd) + 4;

                if (fromEnd < -footerMaxExpectedLen)
                {
                    throw new Exception("Expected to find footer data within the last 512 bytes, but did not find it.");
                }
            }

            if(index == 0)
                throw new Exception("Failed to find the footer position.");

            _dataStream.Seek(index, SeekOrigin.Begin);
            byte[] lenBuf = new byte[8];
            var bytesRead = await _dataStream.ReadAsync(lenBuf, 0, 8);
            if (bytesRead != 8)
                throw new Exception("Unexpected number of bytes read while reading footer!");

            if (BitConverter.IsLittleEndian)
                lenBuf = lenBuf.Reverse().ToArray();

            long footerDataLength = BitConverter.ToInt64(lenBuf, 0);
            if (footerDataLength <= 0 || footerDataLength > footerMaxExpectedLen)
                throw new Exception($"Unexpected header data length read, expected a value between 0 and {footerMaxExpectedLen}!");

            var footerDataStartIndex = index + 12 /* rslt<int64><4 bytes of 0><footer json>*/;

            _dataStream.Seek(footerDataStartIndex, SeekOrigin.Begin);
            var dataBuf = new byte[footerDataLength];
            bytesRead = await _dataStream.ReadAsync(dataBuf, 0, dataBuf.Length);
            if(bytesRead != dataBuf.Length)
                throw new Exception("Could not read the full footer, file ends before the footer ends.");

            var footerDataBytesAsString = Encoding.UTF8.GetString(dataBuf);
            ReplayFooterRaw = footerDataBytesAsString;
            ReplayFooter = JsonConvert.DeserializeObject<ReplayFooter>(footerDataBytesAsString);
            AssignNumericFooterProperties();
            // No additional validation.
        }

        private void ValidateReplayHeaderData(ReplayHeader replayHeader)
        {
            var errors = new List<string>();
            if (string.IsNullOrEmpty(ReplayHeader.Game.Map))
                errors.Add("Map is not set or is empty");

            if (errors.Any())
            {
                var errorStr = string.Join("; ", errors);
                throw new Exception("Replay header data was invalid: " + errorStr);
            }
        }
    }
}
