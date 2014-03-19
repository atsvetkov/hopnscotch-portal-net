namespace Hopnscotch.Portal.Import
{
    public sealed class AmoCrmImportResult
    {
        public AmoCrmImportResult()
        {
            Errors = new AmoCrmImportResultError[0];
            Successful = true;
        }

        public AmoCrmImportResult(AmoCrmImportResultError[] errors)
        {
            Errors = errors;
            if (errors != null && errors.Length > 0)
            {
                Successful = false;
            }
        }

        public bool Successful { get; set; }

        public AmoCrmImportResultError[] Errors { get; set; }
    }
}