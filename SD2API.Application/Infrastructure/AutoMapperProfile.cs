using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoMapper;

namespace SD2API.Application.Infrastructure
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            var mappedModels = LoadMappedModelsFromAssembly(GetType().Assembly);
            mappedModels.ForEach(mm => mm.SetMappings(this));
        }

        private static List<IMappedModel> LoadMappedModelsFromAssembly(Assembly assembly)
        {
            var mappedModels = assembly.GetExportedTypes()
                .Where(t => !t.IsAbstract && !t.IsInterface && typeof(IMappedModel).IsAssignableFrom(t))
                .Select(t => (IMappedModel)Activator.CreateInstance(t)).ToList();
            return mappedModels;
        }
    }
}
