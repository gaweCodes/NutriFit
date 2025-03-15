namespace SharedKernel.Domain;

public interface IEntityKey<TKey> where TKey : struct, IEntityKeyValue;
