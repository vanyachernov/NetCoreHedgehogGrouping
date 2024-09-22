namespace HedgehogsPopulation.Shared;

public class Error
{
    private Error(string message, ErrorType errorType)
    {
        Message = message;
        ErrorType = errorType;
    }

    public string Message { get; set; }
    public ErrorType ErrorType { get; set; }

    public static Error Validation(string message) =>
        new Error(message, ErrorType.Validation);

    public static Error EvensOrOdds(string message) =>
        new Error(message, ErrorType.EvensOrOdds);
    
    public static Error SameColor(string message) =>
        new Error(message, ErrorType.SameColor);
}