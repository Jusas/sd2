using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using SD2API.Application.Core.Replays.Exceptions;
using SD2API.Application.DataExtensions;
using SD2API.Application.Interfaces;
using SD2API.Domain;

namespace SD2API.Application.Core.Replays.Commands
{
    public class CreateReplayHandler : IRequestHandler<CreateReplay, CreateReplayResponse>
    {

        private IApiDbContext _apiDbContext;
        private IBlobStorage _blobStorage;
        private IMapper _mapper;

        public CreateReplayHandler(IApiDbContext apiDbContext, IBlobStorage blobStorage, IMapper mapper)
        {
            _apiDbContext = apiDbContext;
            _blobStorage = blobStorage;
            _mapper = mapper;
        }

        private async Task<string> UploadToStorage(Stream fileStream, string hashStub)
        {
            fileStream.Seek(0, SeekOrigin.Begin);
            var storageUri = await _blobStorage.UploadBlob(fileStream, "files", hashStub + ".rpl3", true);
            return storageUri;
        }

        public async Task<CreateReplayResponse> Handle(CreateReplay request, CancellationToken cancellationToken)
        {
            try
            {
                var fileStream = request.File.Content;
                var parsedReplay = await SD2Tools.ReplayTools.Replay.FromStream(fileStream);
                fileStream.Seek(0, SeekOrigin.Begin);
                var sha = SHA1.Create();
                var now = DateTime.UtcNow;
                var hashSource = BitConverter.ToString(sha.ComputeHash(fileStream))
                                 + $"{DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()}";
                var hashStub = BitConverter.ToString(sha.ComputeHash(Encoding.ASCII.GetBytes(hashSource)))
                    .Replace("-", String.Empty)
                    .Substring(0, 10)
                    .ToLower();

                var blobUrl = await UploadToStorage(fileStream, hashStub);

                var replay = new Replay()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Date = DateTime.UtcNow,
                    ReplayHashStub = hashStub,
                    ReplayRawFooter = parsedReplay.ReplayFooterRaw,
                    ReplayRawHeader = parsedReplay.ReplayHeaderRaw,
                    //ReplayHeader = new ReplayHeader()
                    //{
                    //    Game = parsedReplay.ReplayHeader.Game.ToDomainReplayHeaderGame(), // todo use automapper
                    //    Players = parsedReplay.ReplayHeader.Players.ToDomainReplayPlayerList()
                    //},
                    //ReplayFooter = new ReplayFooter()
                    //{
                    //    result = new ReplayFooter.ReplayFooterResult()
                    //    {
                    //        Duration = parsedReplay.ReplayFooter.Result.Duration,
                    //        Score = parsedReplay.ReplayFooter.Result.Score,
                    //        Victory = parsedReplay.ReplayFooter.Result.Victory
                    //    }
                    //},
                    ReplayHeader = _mapper.Map<ReplayHeader>(parsedReplay.ReplayHeader),
                    ReplayFooter = _mapper.Map<ReplayFooter>(parsedReplay.ReplayFooter),
                    BinaryUrl = blobUrl
                };

                _apiDbContext.Replays.Add(replay);
                await _apiDbContext.SaveChangesAsync(cancellationToken);
                return new CreateReplayResponse()
                {
                    ReplayHash = replay.ReplayHashStub
                };
            }
            catch (Exception e)
            {
                throw new ReplayProcessingFailedException("Processing and saving the replay failed", e);
            }
              
        }
    }
}
