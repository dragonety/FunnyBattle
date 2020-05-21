using System;
using System.Collections.Generic;

namespace Reconstitution {
    public class Components : IDisposable {

        private List<IComponent> temp;
        private List<IComponent> componentList;
        private Dictionary<Type, IComponent> componentDict;

        public Components() {
            temp = new List<IComponent>();
            componentList = new List<IComponent>();
            componentDict = new Dictionary<Type, IComponent>();
        }

        public void OnInit() {
            Dispose();
        }

        public bool IsExist<T>() where T : IComponent {
            return componentDict.ContainsKey(typeof(T));
        }

        public void Dispose() {
            RemoveComponent();
        }

        public void RemoveComponent() {
            temp.Clear();
            temp.AddRange(componentList);
            foreach(IComponent component in temp) {
                component.OnRemove();
            }
            temp.Clear();
            componentList.Clear();
            componentDict.Clear();
        }

        public T AddComponent<T>(T component, IEntity entity) where T : IComponent {
            Type type = component.GetType();
            if (!componentDict.ContainsKey(type)) {
                componentList.Add(component);
                componentDict.Add(type, component);
                component.Entity = entity;
                component.OnInit();
            }
            return (T)component;
        }

        public T AddComponent<T>(IEntity entity) where T : IComponent, new() {
            Type type = typeof(T);
            if (!componentDict.ContainsKey(type)) {
                T component = new T();
                componentList.Add(component);
                componentDict.Add(type, component);
                component.Entity = entity;
                component.OnInit();
                return (T)component;
            }
            return default(T);
        }

        public void RemoveComponent<T>() where T : IComponent {
            Type type = typeof(T);
            IComponent component = null;
            if (componentDict.TryGetValue(type, out component)) {
                component.OnRemove();
                componentList.Remove(component);
                componentDict.Remove(type);
            }
        }

        public T GetComponent<T>() where T : IComponent {
            IComponent component = default(T);
            componentDict.TryGetValue(typeof(T), out component);
            return (T)component;
        }

    }
}