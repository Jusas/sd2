using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using MediatR;

namespace SD2API.Application.Core.Replays.Commands
{
    public class CreateReplay : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public UploadedFile File { get; set; }
        
        public class UploadedFile
        {
            public UploadedFile(string fileName, string contentType, Stream content)
            {
                FileName = fileName;
                ContentType = contentType;
                Content = content;
            }

            public string FileName { get; private set; }
            public string ContentType { get; private set; }
            public Stream Content { get; private set; }

        }
    }
}
