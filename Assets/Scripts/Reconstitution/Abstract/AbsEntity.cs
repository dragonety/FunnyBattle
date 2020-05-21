namespace Reconstitution {

    public abstract class AbsEntity : IEntity {

        private Components components;
        private Features features;
        private Attributes attributes;
        private Configs configs;

        public AbsEntity() {
            components = new Components();
            features = new Features();
            attributes = new Attributes();
            configs = new Configs();
            messageRegister = new MessageRegister();
            updateRegister = new UpdateRegister();
            timerRegister = new TimerRegister();
        }

        public uint Id { get; set; }
        public uint Group { get; set; }
        public MessageRegister messageRegister { get; set; }
        public TimerRegister timerRegister { get; set; }
        public UpdateRegister updateRegister { get; set; }

        public virtual void OnInit() {
            configs.OnInit();
            attributes.OnInit();
            features.OnInit();
            components.OnInit();
            messageRegister.OnInit();
            updateRegister.OnInit();
            timerRegister.OnInit();
        }

        public virtual void OnRemove() {
            components.Dispose();
            features.Dispose();
            attributes.Dispose();
            configs.Dispose();
            messageRegister.Dispose();
            updateRegister.Dispose();
            timerRegister.Dispose();
        }

        public virtual void OnAdd() {

        }

        public T AddAttribute<T>(T attribute) where T : IAttribute {
            return attributes.AddAttribute<T>(attribute);
        }

        public T AddComponent<T>(T component) where T : IComponent {
            return components.AddComponent<T>(component, this);
        }

        public T AddComponent<T>() where T : IComponent, new() {
            return components.AddComponent<T>(this);
        }

        public T AddConfig<T>(T config) where T : IConfig {
            return configs.AddConfig<T>(config);
        }

        public T AddFeature<T>(T feature) where T : IFeature {
            return features.AddFeature<T>(feature, this);
        }

        public void Dispatcher(int id, IBody body = null) {
            messageRegister.Dispatcher(id, body);
        }

        public T GetAttribute<T>() where T : IAttribute {
            return attributes.GetAttribute<T>();
        }

        public T GetComponent<T>() where T : IComponent {
            return components.GetComponent<T>();
        }

        public T GetConfig<T>() where T : IConfig {
            return configs.GetConfig<T>();
        }

        public T GetFeature<T>() where T : IFeature {
            return features.GetFeature<T>();
        }


        public void OnFixedUpdate(float deltaTime) {
            updateRegister.OnFixedUpdate(deltaTime);
        }


        public void OnUpdate(float deltaTime) {
            updateRegister.OnUpdate(deltaTime);
        }

        public void RemoveComponent<T>() where T : IComponent {
            components.RemoveComponent<T>();
        }

        public void RemoveComponent() {
            components.RemoveComponent();
        }
    }

}