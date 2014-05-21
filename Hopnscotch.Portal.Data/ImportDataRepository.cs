using System.Data.Entity;
using System.Linq;
using Hopnscotch.Portal.Contracts;
using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Data
{
    public sealed class ImportDataRepository : EFRepository<ImportData>, IImportDataRepository
    {
        public ImportDataRepository(DbContext context)
            : base(context)
        {
        }

        public ImportData GetByResponseType(string responseType)
        {
            return Entities.FirstOrDefault(i => i.ResponseType == responseType);
        }
    }
}