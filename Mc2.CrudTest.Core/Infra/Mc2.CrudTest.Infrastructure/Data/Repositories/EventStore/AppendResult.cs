namespace Mc2.CrudTest.Infrastructure.Data.Repositories.EventStore;

public sealed class AppendResult
{
    public AppendResult(long nextExpectedVersion)
    {
        NextExpectedVersion = nextExpectedVersion;
    }

    public long NextExpectedVersion { get; }
}
