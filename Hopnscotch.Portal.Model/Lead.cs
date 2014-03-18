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

    public class Contact : NamedBusinessEntityBase
    {
        public int LevelId { get; set; }

        public virtual ICollection<Lead> Leads { get; set; }
    }

    public class Task : NamedBusinessEntityBase
    {
        public string Text { get; set; }
        public DateTime CompleteUntil { get; set; }
        
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public int ElementId { get; set; }
        public ElementTypes ElementType { get; set; }
        
        public int? LeadId { get; set; }
        public int? ContactId { get; set; }
    }

    public enum ElementTypes
    {
        Contact = 1,
        Lead = 2
    }

    // TODO: add lookups (custom field tables):
    // * Lead -> Level (Beginner, etc)
    // * Contact -> Level (Beginner, etc)
    // * Lead -> Group type (Individually, Group, Pair)
    // * Lead -> Status (Учится, Лист ожидания и т.д.)
}