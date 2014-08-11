using System.Configuration;
using Hopnscotch.Portal.Contracts;

namespace Hopnscotch.Portal.Web
{
    internal sealed class Config : IConfig
    {
        public string AmoSubDomain
        {
            get { return ConfigurationManager.AppSettings["AmoSubDomain"]; }
        }

        public string AmoLogin
        {
            get { return ConfigurationManager.AppSettings["AmoLogin"]; }
        }

        public string AmoHash
        {
            get { return ConfigurationManager.AppSettings["AmoHash"]; }
        }

        public string DbConnectionString
        {
            get
            {
                var connectionStringSettingName = ConfigurationManager.AppSettings["ConnectionStringSettingName"];
                
                return ConfigurationManager.AppSettings[connectionStringSettingName];
            }
        }
    }
}