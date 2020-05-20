public interface MoveComponent : BaseComponent {
}

public interface NetComponent : BaseComponent {
}

public interface GraphicComponent : BaseComponent {

    void GetStab();
    void GetHurt();

}

public interface PhysicsComponent : BaseComponent {
}