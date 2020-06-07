public interface BaseComponent : IUpdate, IInit{
}

public interface IInit {
    void OnInit(Player player);
}

public interface IUpdate {
    void OnUpdate(float delta);
}

public interface IFixedUpdate {
    void OnFixedUpdate(float delta);
}

//public interface I