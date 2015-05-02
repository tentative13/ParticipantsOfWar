using Microsoft.Practices.Unity;
using ParticipantsOfWar.BLL;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;

namespace ParticipantsOfWar
{
	public static class UnityConfig
	{
		public static UnityContainer container { get; private set; }

		public static void RegisterComponents()
		{
			var container = new UnityContainer();
			
			// register all your components with the container here
			// it is NOT necessary to register your controllers
			
			// e.g. container.RegisterType<ITestService, TestService>();
			container.RegisterType<IArchiveRepository, ArchiveRepository>();
			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
		}
	}
}