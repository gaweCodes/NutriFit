namespace SharedKernel.Domain;

public class ValidationException(string message) : DomainException(message);