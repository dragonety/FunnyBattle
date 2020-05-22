using UnityEngine;

namespace Reconstitution {

    public sealed class EntityFactory {
        
        public static Entity CreatePlayer(uint id, uint group, float health, GameObject gameObject, bool isLocalPlayer, bool isClient) {

            Entity entity = new Entity(id, group);
            entity.OnInit();

            entity.AddAttribute<HealthAttribute>(new HealthAttribute(health));
            entity.AddFeature<ObjectFeature>(new ObjectFeature(gameObject));

            if (isLocalPlayer) {
                Debug.Log("entity " + id + " add component " + " localMoveComponent");
                entity.AddComponent<PlayerLocalMoveComponent>();
            } else {
                //entity.AddComponent()
            }

            if (isClient) {
                //entity.AddComponent
            }

            return entity;

        }

    }

}