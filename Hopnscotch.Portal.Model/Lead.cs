using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hopnscotch.Portal.Model
{
    public class Lead : BusinessEntityBase, IUpdatableFrom<Lead>
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

        [NotMapped]
        public DayOfWeek[] Days { get; set; }

        [NotMapped]
        public int? Duration { get; set; }

        protected override void CopyValuesFromSpecific(BusinessEntityBase entity)
        {
            var lead = entity as Lead;
            if (lead == null)
            {
                return;
            }

            this.Name = lead.Name;
            this.Price = lead.Price;
            this.StartDate = lead.StartDate;
            this.EndDate = lead.EndDate;
            this.AmoLevelId = lead.AmoLevelId;
            this.LanguageLevel = lead.LanguageLevel;
            this.ScheduleText = lead.ScheduleText;
        }

        public void CopyValuesFrom(Lead entity)
        {
            CopyValuesInternal(entity);
        }
    }

    // TODO: add lookups (custom field tables):
    // * Lead -> Level (Beginner, etc)
    // * Contact -> Level (Beginner, etc)
    // * Lead -> Group type (Individually, Group, Pair)
    // * Lead -> Status (Учится, Лист ожидания и т.д.)
}