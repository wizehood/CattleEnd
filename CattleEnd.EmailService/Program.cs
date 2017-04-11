using System;
using System.IO;
using Topshelf;

namespace CattleEnd.EmailService
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataDirectory = string.Concat(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName, "\\CattleEnd.DataAccessLayer\\Data\\");
            AppDomain.CurrentDomain.SetData("DataDirectory", dataDirectory);

            HostFactory.Run(x =>
            {
                x.Service<Startup>(s =>
                {
                    s.ConstructUsing(name => new Startup());
                    s.WhenStarted(ls => ls.OnStart());
                    s.WhenStopped(ls => ls.OnStop());
                });

                x.RunAsLocalSystem();
                x.StartAutomatically();

                x.SetDescription(ServiceConstants.Description);
                x.SetDisplayName(ServiceConstants.DisplayName);
                x.SetServiceName(ServiceConstants.ServiceName);
            });
        }

        public static class ServiceConstants
        {
            public const string ServiceName = "Email_Notification";
            public const string DisplayName = "Email notification service";
            public const string Description = "Service for sending warrior guard notifications";
        }
    }
}
