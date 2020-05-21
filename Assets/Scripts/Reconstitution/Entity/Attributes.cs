using System;
using System.Collections.Generic;

namespace Reconstitution {
    public class Attributes : IDisposable {

        private List<IAttribute> attributeList;
        private Dictionary<Type, IAttribute> attributeDict;

        public Attributes() {
            attributeList = new List<IAttribute>();
            attributeDict = new Dictionary<Type, IAttribute>();
        }

        public void OnInit() {
            Dispose();
        }

        public void Dispose() {
            attributeList.Clear();
            attributeDict.Clear();
        }

        public T AddAttribute<T>(T attribute) where T : IAttribute {
            //Type type = typeof(T);
            Type type = attribute.GetType();
            if (!attributeDict.ContainsKey(type)) {
                attributeList.Add(attribute);
                attributeDict.Add(type, attribute);
                return (T)attribute;
            }
            return default(T);
        }

        public T GetAttribute<T>() where T : IAttribute {
            IAttribute attribute = default(T);
            attributeDict.TryGetValue(typeof(T), out attribute);
            return (T)attribute;
        }

    }
}