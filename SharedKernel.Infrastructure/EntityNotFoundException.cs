namespace SharedKernel.Infrastructure;

public class EntityNotFoundException(string entityName, Guid key) : Exception($"{entityName} mit id '{key}' wurde nicht gefunden.");
