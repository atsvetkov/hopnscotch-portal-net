using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Level : AmoEntityBase, IUpdatableFrom<Level>
    {
        public Level()
        {
            Leads = new List<Lead>();
        }

        public string Name { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }

        public void CopyValuesFrom(Level entity)
        {
            if (entity == null)
            {
                return;
            }

            this.Name = entity.Name;
        }
    }
}