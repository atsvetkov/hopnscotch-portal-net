using System;
using System.IO;

namespace Hopnscotch.Portal.ConsoleTester
{
    internal class AmoConfig
    {
        private const string subDomain = "subDomain";
        private const string login = "login";
        private const string hash = "hash";

        public string SubDomain { get; private set; }
        public string Login { get; private set; }
        public string Hash { get; private set; }

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