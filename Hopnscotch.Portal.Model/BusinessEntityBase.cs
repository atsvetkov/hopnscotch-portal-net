using System;

namespace Hopnscotch.Portal.Model
{
    public abstract class BusinessEntityBase : AmoEntityBase
    {
        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        public int AmoResponsibleUserId { get; set; }

        public int ResponsibleUserId { get; set; }

        public int AccountId { get; set; }

        public User ResponsibleUser { get; set; }

        protected void CopyValuesInternal(BusinessEntityBase entity)
        {
            this.Created = entity.Created;
            this.Modified = entity.Modified;
            this.AmoResponsibleUserId = entity.AmoResponsibleUserId;
            this.AccountId = entity.AccountId;
            this.ResponsibleUser = entity.ResponsibleUser;

            CopyValuesFromSpecific(entity);
        }

        protected virtual void CopyValuesFromSpecific(BusinessEntityBase entity)
        {
            
        }
    }
}