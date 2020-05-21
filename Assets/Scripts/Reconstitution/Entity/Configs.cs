using System;
using System.Collections.Generic;

namespace Reconstitution {
    public class Configs : IDisposable {

        private List<IConfig> configList;
        private Dictionary<Type, IConfig> configDict;

        public Configs() {
            configList = new List<IConfig>();
            configDict = new Dictionary<Type, IConfig>();
        }

        public void OnInit() {
            Dispose();
        }

        public void Dispose() {
            configList.Clear();
            configDict.Clear();
        }

        public T AddConfig<T>(T config) where T : IConfig {
            Type type = config.GetType();
            if (!configDict.ContainsKey(type)) {
                configList.Add(config);
                configDict.Add(type, config);
                return (T)config;
            }
            return default(T);
        }

        public T GetConfig<T>() where T : IConfig {
            IConfig config = default(T);
            configDict.TryGetValue(typeof(T), out config);
            return (T)config;
        }

        // ???
        public List<IConfig> Copy() {
            return null;
        }

    }
}