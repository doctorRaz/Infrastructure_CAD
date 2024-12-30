using Infrastructure.Extensions.Entities;
using Infrastructure.Extensions.Interfaces;
using System.Reflection;

namespace drz.Reflection.Infrastructure
{
    internal class CommandInfoRepository : ICommandInfoRepository
    {
        public IEnumerable<CommandInfo> Get(IEnumerable<Assembly> assemblies)
        {
            List<CommandInfo> commands = new List<CommandInfo>();
            foreach (Assembly assembly in assemblies)
            {
                Type[] expTypes = assembly.GetTypes();
                foreach (Type t in expTypes)
                {

                }
            }
            throw new NotImplementedException();
        }
    }
}
