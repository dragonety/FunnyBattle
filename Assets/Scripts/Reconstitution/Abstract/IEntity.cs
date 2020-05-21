namespace Reconstitution {

    public interface IEntity : ILifecycle, IUpdate, IFixedUpdate, IMessage {

        uint Id { get; }
        uint Group { get; }

        void OnAdd();

        /*  Component的 增 删 查
         * 
         */
        T AddComponent<T>(T component) where T : IComponent;

        T AddComponent<T>() where T : IComponent, new();

        void RemoveComponent<T>() where T : IComponent;

        void RemoveComponent();

        T GetComponent<T>() where T : IComponent;

        /*  Config的 增 查
         * 
         */
        T AddConfig<T>(T config) where T : IConfig;

        T GetConfig<T>() where T : IConfig;

        /*  Attribute的 增 查
         * 
         */
        T AddAttribute<T>(T attribute) where T : IAttribute;

        T GetAttribute<T>() where T : IAttribute;

        /*  Feature的 增 查
         * 
         */
        T AddFeature<T>(T feature) where T : IFeature;

        T GetFeature<T>() where T : IFeature;

        /*  事件的注册器
         * 
         */
        MessageRegister messageRegister { get; }
        TimerRegister timerRegister { get; }
        UpdateRegister updateRegister { get; }
    }

}