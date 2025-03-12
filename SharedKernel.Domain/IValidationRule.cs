namespace SharedKernel.Domain;

public interface IValidationRule
{
    bool IsBroken();
    string Message { get; }
}