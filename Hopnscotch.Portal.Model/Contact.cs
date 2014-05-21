using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Contact : BusinessEntityBase, IUpdatableFrom<Contact>
    {
        public Contact()
        {
            Leads = new List<Lead>();
            Attendances = new List<Attendance>();
        }

        public int LevelId { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }
        public virtual ICollection<Attendance> Attendances { get; set; }

        protected override void CopyValuesFromSpecific(BusinessEntityBase entity)
        {
            var contact = entity as Contact;
            if (contact == null)
            {
                return;
            }

            this.LevelId = contact.LevelId;
        }

        public void CopyValuesFrom(Contact entity)
        {
            CopyValuesInternal(entity);
        }
    }
}