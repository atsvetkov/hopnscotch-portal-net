namespace Hopnscotch.Portal.Integration.AmoCRM.Entities
{
    public abstract class ApiListResponseBase<T>
    {
        public abstract T[] Entities { get; set; }
    }
}
