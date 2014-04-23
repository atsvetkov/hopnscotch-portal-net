namespace Hopnscotch.Portal.Model
{
    public interface IUpdatableFrom<in T> where T : AmoEntityBase
    {
        void CopyValuesFrom(T entity);
    }
}