namespace SharedKernel.Domain;

public interface IEventRegistryAccessor
{
    IEventRegistry GetEventRegistry();
}
