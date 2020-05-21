using UnityEngine;

namespace Reconstitution {

    public sealed class EntityFactory {
        
        public static Entity CreatePlayer(uint id, uint group, float health, GameObject gameObject, bool isLocalPlayer, bool isClient) {

            Entity entity = new Entity(id, group);
            entity.OnInit();
            //entity.AddAttribute()
            //entity.AddFeature()
            if (isLocalPlayer) {
                //entity.AddComponent()
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