namespace Hopnscotch.Portal.Contracts
{
    public interface IConfig
    {
        string AmoSubDomain { get; }

        string AmoHash { get; }

        string AmoLogin { get; }
    }
}
