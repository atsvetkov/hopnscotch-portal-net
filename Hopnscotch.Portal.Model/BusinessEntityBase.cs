using System;

namespace Hopnscotch.Portal.Model
{
    public abstract class BusinessEntityBase : AmoEntityBase
    {
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
        public int ResponsibleUserId { get; set; }
        public int AccountId { get; set; }
    }
}