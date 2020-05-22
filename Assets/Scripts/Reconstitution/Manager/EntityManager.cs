using System.Collections.Generic;
using UnityEngine;

namespace Reconstitution {
    public class EntityManager {

        private static Entities entities;

        public static void Init() {
            entities = new Entities();
            UpdateManager.RegisterUpdate(OnUpdate);
            UpdateManager.RegisterFixedUpdate(OnFixedUpdate);
        }

        public static void Dispose() {
            entities.Dispose();
            entities = null;
            UpdateManager.UnregisterUpdate(OnUpdate);
            UpdateManager.UnregisterFixedUpdate(OnFixedUpdate);
        }

        public static void OnUpdate(float deltaTime) {
            if (entities != null) {
                entities.OnUpdate(deltaTime);
            }            
        }

        public static void OnFixedUpdate(float deltaTime) {
            if (entities != null) {
                entities.OnFixedUpdate(deltaTime);
            }
        }

        public static Entity AddEntity(Entity entity) {
            if (entities != null) {
                return entities.AddEntity(entity);
            } else {
                return null;
            }
        }

        public static void RemoveEntity(uint id) {
            if (entities != null) {
                entities.RemoveEntity(id);
            }
        }

        public static Entity GetEntity(uint id) {
            if (entities != null) {
                return entities.GetEntity(id);
            } else {
                return null;
            }
        }

        public static List<Entity> GetEntityGroup(uint group) {
            if (entities != null) {
                return entities.GetEntityGroup(group);
            } else {
                return null;
            }
        }

        public static void Dispatcher(int mid) {
            if (entities != null) {
                entities.Dispatcher(mid, 0, null);
            }
        }

        public static void Dispatcher(int mid, uint id) {
            if (entities != null) {
                entities.Dispatcher(mid, id, null);
            }
        }

        public static void Dispatcher(int mid, IBody body) {
            if (entities != null) {
                entities.Dispatcher(mid, 0, body);
            }
        }

        public static void Dispatcher(int mid, uint id, IBody body) {
            if (entities != null) {
                entities.Dispatcher(mid, id, body);
            }
        }

    }

}