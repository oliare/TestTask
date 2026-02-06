namespace TestTask.Application.Exceptions;

public class EntityNotFoundException : Exception
{
    public EntityNotFoundException(string entity, object? id)
        : base($"Entity {entity} with id {id} was not found") { }
}

