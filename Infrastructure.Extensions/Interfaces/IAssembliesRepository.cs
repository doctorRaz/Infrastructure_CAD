using System.Collections.Generic;
using System.Reflection;

namespace Infrastructure.Extensions.Interfaces
{
    public interface IAssembliesRepository
    {
        IEnumerable<Assembly> Get();
    }
}
