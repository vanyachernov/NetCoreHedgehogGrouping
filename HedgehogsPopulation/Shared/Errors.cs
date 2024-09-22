namespace HedgehogsPopulation.Shared;

public static class Errors
{
    public static Error ValueIsInvalid(string? name = null)
    {
        var paramName = name ?? "value";
        return Error.Validation($"{paramName} is invalid");
    }

    public static Error ValuesIsEverOrOdd()
    {
        return Error.EvensOrOdds("Parity don't match, " +
                                 "so it is impossible to bring all to the same colour!");
    }

    public static Error SameColor()
    {
        return Error.SameColor("The hedgehogs are already the same colour!");
    }
}