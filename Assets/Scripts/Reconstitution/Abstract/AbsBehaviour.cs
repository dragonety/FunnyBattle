using System.Collections.Generic;

namespace Reconstitution {
    public abstract class AbsBehaviour : AbsCompose {

        private List<ICompose> moduleList;
        private Dictionary<System.Type, ICompose> moduleDict;

        public AbsBehaviour() {
            moduleList = new List<ICompose>();
            moduleDict = new Dictionary<System.Type, ICompose>();
        }

        public override void OnInit() {
            
        }

        public override void OnRemove() {
            foreach(ICompose module in moduleList) {
                module.OnRemove();
            }
            moduleDict.Clear();
            moduleList.Clear();
        }

        public override void OnUpdate(float deltaTime) {
            foreach(ICompose module in moduleList) {
                module.OnUpdate(deltaTime);
            }
        }

        public override void OnFixedUpdate(float deltaTime) {
            foreach(ICompose module in moduleList) {
                module.OnFixedUpdate(deltaTime);
            }
        }

        public ICompose Add<T>() where T : ICompose, new() {
            System.Type type = typeof(T);
            if (!moduleDict.ContainsKey(type)) {
                ICompose module = new T();
                moduleList.Add(module);
                moduleDict.Add(type, module);
                module.OnInit();
                return module;
            }
            return default(T);
        }

        public void Remove<T>() where T : ICompose {
            System.Type type = typeof(T);
            ICompose module = null;
            if (moduleDict.TryGetValue(type, out module)) {
                moduleList.Remove(module);
                moduleDict.Remove(type);
                module.OnRemove();
            }
        }

    }
}