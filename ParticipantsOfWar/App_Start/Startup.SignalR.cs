using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.AspNet.SignalR;

namespace ParticipantsOfWar
{
 public partial class Startup
    {
        private static void ConfigureSignalr(IAppBuilder app)
        {
            var hubConfiguration = new HubConfiguration();
            hubConfiguration.EnableDetailedErrors = true;
            app.MapSignalR(hubConfiguration);

        }
    }
}
