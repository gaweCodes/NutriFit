namespace SharedKernel.Domain;

public class EntityNotFoundException(string entityName, Guid key) : DomainException($"{entityName} mit id '{key}' wurde nicht gefunden.");
