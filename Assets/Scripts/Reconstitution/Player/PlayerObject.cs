using UnityEngine.Networking;

namespace Reconstitution {
    public class PlayerObject : NetworkBehaviour {

        [SyncVar(hook = "OnHealthChange")]
        public float health;

        public override void OnStartServer() {
            health = Consts.health;
        }

        private void Start() {
            EntityManager.AddEntity(EntityFactory.CreatePlayer(netId.Value, GroupID.player, health, gameObject, isLocalPlayer, isClient));
        }

        private void OnDestroy() {
            EntityManager.RemoveEntity(netId.Value);
            if (isLocalPlayer) {
                UnetManager.instance.StopClient();
            }
        }

        [Client]
        private void OnHealthChange(float health) {
            Entity entity = EntityManager.GetEntity(netId.Value);
            if (entity != null) {
                HealthAttribute attribute = entity.GetAttribute<HealthAttribute>();
                attribute.curHealth = health;
                EntityManager.Dispatcher(MessageID.HealthUpdate, netId.Value);
            }
        }

        //RpcDie

    }
}