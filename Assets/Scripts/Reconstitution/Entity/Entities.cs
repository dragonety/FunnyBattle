using System;
using System.Collections.Generic;

namespace Reconstitution {
    public class Entities : IDisposable, IUpdate, IFixedUpdate {

        private List<Entity> temp;
        private List<Entity> entityList;
        private Dictionary<uint, Entity> entityDict;
        private Dictionary<uint, List<Entity>> entityGroupDict;

        public Entities() {
            temp = new List<Entity>();
            entityList = new List<Entity>();
            entityDict = new Dictionary<uint, Entity>();
            entityGroupDict = new Dictionary<uint, List<Entity>>();
        }

        public void Dispose() {
            entityGroupDict.Clear();
            entityDict.Clear();
            entityList.Clear();
            temp.Clear();
        }

        public void OnUpdate(float deltaTime) {
            Refill();
            foreach(Entity entity in temp) {
                entity.OnUpdate(deltaTime);
            }
        }

        public void OnFixedUpdate(float deltaTime) {
            Refill();
            foreach (Entity entity in temp) {
                entity.OnFixedUpdate(deltaTime);
            }
        }


        /**
         *  message的 dispatcher只包含消息id 和消息内容body 但是entity中的消息需要确认entity的uid,从而达到指定消息传送的目的
         *  如果指定了entity的UID就只发送到指定UID上，没有指定就发送到所有同一种类的Entity上
         */
        public void Dispatcher(int id, uint uid, IBody body) {
            if (uid > 0) {
                Entity entity = null;
                if (entityDict.TryGetValue(uid, out entity)) {
                    entity.Dispatcher(id, body);
                }
            } else {
                Refill();
                foreach(Entity entity in temp) {
                    entity.Dispatcher(id, body);
                }
            }            
        }

        private void Refill() {
            temp.Clear();
            temp.AddRange(entityList);
        }

        /**
         *  entity的 增 删 查
         */
        public Entity AddEntity(Entity entity) {
            if (!entityDict.ContainsKey(entity.Id)) {
                entityList.Add(entity);
                entityDict.Add(entity.Id, entity);
                GetEntityGroup(entity.Group).Add(entity);
                entity.OnAdd();
                return entity;
            }
            return null;
        }

        public void RemoveEntity(uint id) {
            Entity entity = null;
            if (entityDict.TryGetValue(id, out entity)) {
                entityList.Remove(entity);
                entityDict.Remove(id);
                GetEntityGroup(entity.Group).Remove(entity);
            }
        }

        public Entity GetEntity(uint id) {
            Entity entity = null;
            entityDict.TryGetValue(id, out entity);
            return entity;
        }

        public List<Entity> GetEntityGroup(uint group) {
            List<Entity> entityGroup = null;
            if (!entityGroupDict.TryGetValue(group, out entityGroup)) {
                entityGroup = new List<Entity>();
                entityGroupDict.Add(group, entityGroup);
            }
            return entityGroup;
        }

    }
}