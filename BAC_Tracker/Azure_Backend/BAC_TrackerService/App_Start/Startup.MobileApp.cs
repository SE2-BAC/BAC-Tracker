using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using BAC_TrackerService.DataObjects;
using BAC_TrackerService.Models;
using Owin;

namespace BAC_TrackerService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new BAC_TrackerInitializer());
            Database.SetInitializer(new BAC_TrackerInitializer2());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<BAC_TrackerContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class BAC_TrackerInitializer : CreateDatabaseIfNotExists<BAC_TrackerContext>
    {
        protected override void Seed(BAC_TrackerContext context)
        {
            List<TodoItem> todoItems = new List<TodoItem>
            {
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "First item", Complete = false },
                new TodoItem { Id = Guid.NewGuid().ToString(), Text = "Second item", Complete = false },
            };

            foreach (TodoItem todoItem in todoItems)
            {
                context.Set<TodoItem>().Add(todoItem);
            }

            base.Seed(context);
        }
    }

    public class BAC_TrackerInitializer2 : DropCreateDatabaseAlways<BAC_TrackerContext>
    {
        protected override void Seed(BAC_TrackerContext context)
        {
            List<AlcoholTest> booze = new List<AlcoholTest>
            {
                new AlcoholTest { Id = Guid.NewGuid().ToString(), Name = "Beer", Volume = 0.8f, Finished = false },
                new AlcoholTest { Id = Guid.NewGuid().ToString(), Name = "Booze", Volume = 2f, Finished = false },
                new AlcoholTest { Id = Guid.NewGuid().ToString(), Name = "The Fuck", Volume = 20f, Finished = true },
            };

            foreach (AlcoholTest test in booze)
            {
                context.Set<AlcoholTest>().Add(test);
            }

            base.Seed(context);
        }
    }
}

