using System;
using System.Data.Entity;
using System.Linq;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class LeadRepository : EFRepository<Lead>, ILeadRepository
    {
        public LeadRepository(DbContext context) : base(context)
        {
        }

        public Lead GetByAmoId(int amoId)
        {
            return Entities.FirstOrDefault(l => l.AmoId == amoId);
        }
    }
}