using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class User : AmoEntityBase
    {
        public User()
        {
            Leads = new List<Lead>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Login { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }
    }
}