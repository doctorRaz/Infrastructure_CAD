using Infrastructure.Extensions.Entities;
using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.Extensions.Interfaces
{
    public interface ICommandInfoRepository
    {
        IEnumerable<CommandInfo> Get(IEnumerable<Assembly> assemblies);
    }
}
