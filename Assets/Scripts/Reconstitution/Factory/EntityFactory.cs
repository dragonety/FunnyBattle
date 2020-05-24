using UnityEngine;

namespace Reconstitution {

    public sealed class EntityFactory {
        
        public static Entity CreatePlayer(uint id, uint group, float health, GameObject gameObject, bool isLocalPlayer, bool isClient) {

            Entity entity = new Entity(id, group);
            entity.OnInit();

            entity.AddAttribute<HealthAttribute>(new HealthAttribute(health));
            entity.AddFeature<ObjectFeature>(new ObjectFeature(gameObject));
            entity.AddFeature<AnimatorFeature>(new AnimatorFeature(gameObject));

            if (isLocalPlayer) {

                // 之后整合到别的地方
                Camera.main.GetComponent<CameraController>().transPlayer = entity.GetFeature<ObjectFeature>().transform;

                Debug.Log("entity " + id + " add component " + " localMoveComponent");
                entity.AddComponent<PlayerLocalMoveComponent>();
                entity.AddComponent<PlayerLocalAnimatorComponent>();
            } else {
                //entity.AddComponent()
                entity.AddComponent<PlayerSyncMoveComponent>();
                entity.AddComponent<PlayerHurtComponent>();
            }

            if (isClient) {
                //entity.AddComponent<PlayerHurtComponent>();
            }

            return entity;

        }

        public static Entity CreateSword(uint id, uint group, GameObject gameObject, bool isLocalPlayer) {

            Entity entity = new Entity(id, group);
            entity.OnInit();

            entity.AddFeature<ObjectFeature>(new ObjectFeature(gameObject));
            if (isLocalPlayer) {
                entity.AddComponent<SwordAttackComponent>();
            }
            return entity;
		}

    }

}