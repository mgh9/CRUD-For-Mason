namespace Mc2.CrudTest.Domain.Abstractions.Guards;

public static partial class GuardClauseExtensions
{
    public static T NotFound<T>(this IGuardClause guardClause, T? aggregate, string? message = null) where T : class
    {
        if (aggregate is null)
        {
            NotFound(message ?? "Not found");
        }

        return aggregate!;
    }
}
