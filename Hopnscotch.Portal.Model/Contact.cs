using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Contact : NamedBusinessEntityBase
    {
        public Contact()
        {
            Leads = new List<Lead>();
            Attendances = new List<Attendance>();
        }

        public int LevelId { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }
    }
}