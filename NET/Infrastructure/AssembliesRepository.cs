using Infrastructure.Extensions.Interfaces;
using System.Reflection;

namespace drz.Reflection.Infrastructure
{
    internal class AssembliesRepository : IAssembliesRepository
    {
        public IEnumerable<Assembly> Get()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}
