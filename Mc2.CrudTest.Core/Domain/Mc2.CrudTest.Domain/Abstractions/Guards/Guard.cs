namespace Mc2.CrudTest.Domain.Abstractions.Guards;

public sealed class Guard : IGuardClause
{
    public static IGuardClause Against { get; } = new Guard();

    private Guard() { }
}
