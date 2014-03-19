using Hopnscotch.Portal.Integration.AmoCRM.Entities;

namespace Hopnscotch.Portal.Import
{
    public sealed class AmoCrmImportResultError
    {
        public string Message { get; set; }
        
        public int AmoId { get; set; }

        public AmoCrmEntityTypes EntityType { get; set; }
    }
}