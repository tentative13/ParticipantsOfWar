using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Practices.Unity;
using ParticipantsOfWar.BLL;
using ParticipantsOfWar.DAL;
using ParticipantsOfWar.Models;
using System.Web.Http;
using System.Web.Mvc;
using Unity.WebApi;
using System;
using System.Data.Entity;
using Microsoft.Practices.Unity.Configuration;
using ParticipantsOfWar.Controllers;

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
			container.RegisterType<IUserStore<ApplicationUser>, UserStore<ApplicationUser>>();
			container.RegisterType<UserManager<ApplicationUser>>();
			container.RegisterType<DbContext, ArchiveContext>();
			container.RegisterType<ApplicationUserManager>();
			container.RegisterType<AccountController>(new InjectionConstructor());
			GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
		}
	}
}