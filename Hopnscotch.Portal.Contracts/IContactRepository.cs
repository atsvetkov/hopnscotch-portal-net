using Hopnscotch.Portal.Model;

namespace Hopnscotch.Portal.Contracts
{
    public interface IContactRepository : IRepository<Contact>
    {
        Contact GetByAmoId(int amoId);
    }

    public interface IImportDataRepository : IRepository<ImportData>
    {
        ImportData GetByResponseType(string responseType);
    }
}