using System;

namespace Hopnscotch.Portal.Integration.AmoCRM
{
    public class ImportSimulationException : Exception
    {
        public ImportSimulationException(string message)
            : base(message)
        {
        }
    }

    public class AttributeMissingException : Exception
    {
        public AttributeMissingException(string message)
            : base(message)
        {
        }
    }
}
