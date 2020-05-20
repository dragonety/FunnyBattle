namespace Reconstitution {

    public interface ILifecycle {
        void OnInit();
        void OnRemove();
    }

    public interface IUpdate {
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdate {
        void OnFixedUpdate(float deltaTime);
    }

    public interface ICompose : ILifecycle, IUpdate, IFixedUpdate {

    }

}