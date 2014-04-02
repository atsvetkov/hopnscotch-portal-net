using System;

namespace Hopnscotch.Portal.Model
{
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

        public virtual Lead Lead { get; set; }
        public virtual Contact Contact { get; set; }
    }
}