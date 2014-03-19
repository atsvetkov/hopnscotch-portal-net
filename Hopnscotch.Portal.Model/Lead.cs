using System;
using System.Collections.Generic;

namespace Hopnscotch.Portal.Model
{
    public class Lead : NamedBusinessEntityBase
    {
        public double Price { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public int LevelId { get; set; }
        public int GroupTypeId { get; set; }
        public string ScheduleText { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; }
    }

    // TODO: add lookups (custom field tables):
    // * Lead -> Level (Beginner, etc)
    // * Contact -> Level (Beginner, etc)
    // * Lead -> Group type (Individually, Group, Pair)
    // * Lead -> Status (Учится, Лист ожидания и т.д.)
}