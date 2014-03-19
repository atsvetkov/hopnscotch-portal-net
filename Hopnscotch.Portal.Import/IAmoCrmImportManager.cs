using System;

namespace Hopnscotch.Portal.Import
{
    public interface IAmoCrmImportManager : IDisposable
    {
        AmoCrmImportResult Import(AmoCrmImportOptions options);
    }
}