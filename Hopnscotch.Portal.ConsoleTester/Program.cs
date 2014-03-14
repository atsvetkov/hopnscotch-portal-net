using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hopnscotch.Integration.AmoCRM;
using Hopnscotch.Integration.AmoCRM.DataProvider;

namespace Hopnscotch.Portal.ConsoleTester
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }
        
        static async Task RunAsync()
        {
            var config = new AmoConfig("..\\..\\..\\amo.txt");
            var subDomain = config.SubDomain;
            var login = config.Login;
            var hash = config.Hash;
            IAmoDataProvider amoDataProvider = new AmoDataProvider(subDomain, login, hash);

            var result = await amoDataProvider.AuthenticateAsync();
            var contacts = await amoDataProvider.GetContactsAsync();
            var leads = await amoDataProvider.GetLeadsAsync();
            var tasks = await amoDataProvider.GetTasksAsync();

            var numberOfLeadsWithPrice = leads.Response.Leads.Count(l => l.Price > 0);
            var latestLead = leads.Response.Leads.OrderByDescending(l => l.Created).First();
        }
    }

    internal class AmoConfig
    {
        private const string subDomain = "subDomain";
        private const string login = "login";
        private const string hash = "hash";

        public string SubDomain { get; set; }
        public string Login { get; set; }
        public string Hash { get; set; }

        public AmoConfig(string path)
        {
            try
            {
                foreach (var line in File.ReadAllLines(path))
                {
                    var parts = line.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length != 2)
                    {
                        Console.Out.WriteLine("Wrong config line ignored");
                        continue;
                    }

                    if (parts[0].Trim() == subDomain)
                    {
                        SubDomain = parts[1].Trim();
                    }

                    if (parts[0].Trim() == login)
                    {
                        Login = parts[1].Trim();
                    }

                    if (parts[0].Trim() == hash)
                    {
                        Hash = parts[1].Trim();
                    }
                }
            }
            catch (Exception e)
            {
                Console.Out.WriteLine("Error reading config file. " + e.Message);
            }
            
        }
    }
}
