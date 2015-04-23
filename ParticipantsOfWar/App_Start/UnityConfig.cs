using Microsoft.Practices.Unity;
using ParticipantsOfWar.BLL;
using System.Web.Http;
using Unity.WebApi;

namespace ParticipantsOfWar
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IArchiveRepository, ArchiveRepository>();
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}