using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hopnscotch.Portal.Model
{
    public class Lead : NamedBusinessEntityBase
    {
        public Lead()
        {
            Contacts = new List<Contact>();
            Lessons = new List<Lesson>();
        }

        public double Price { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int? AmoLevelId { get; set; }
        public int? LanguageLevelId { get; set; }
        
        public string ScheduleText { get; set; }

        //public int GroupTypeId { get; set; }
        
        public Level LanguageLevel { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }

    // TODO: add lookups (custom field tables):
    // * Lead -> Level (Beginner, etc)
    // * Contact -> Level (Beginner, etc)
    // * Lead -> Group type (Individually, Group, Pair)
    // * Lead -> Status (Учится, Лист ожидания и т.д.)
}