namespace Reconstitution {
    public interface IComponent : ILifecycle {
        IEntity Entity { get; set; }
    }
}