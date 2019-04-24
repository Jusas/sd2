using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace SD2API.Application.Infrastructure
{
    public interface IMappedModel
    {
        void SetMappings(Profile mappings);
    }
}
