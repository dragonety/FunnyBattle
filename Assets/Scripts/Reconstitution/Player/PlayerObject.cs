using UnityEngine.Networking;
using UnityEngine;

namespace Reconstitution {
    public class PlayerObject : NetworkBehaviour {

        [SyncVar(hook = "OnHealthChange")]
        public float health;

        private bool debug = true;

        [SerializeField]
        private RectTransform healthBar;

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
            healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        }

        public void GetStab(uint targetId) {
            CmdGetStab(targetId);
        }

        [Command]
        private void CmdGetStab(uint targetId) {
            RpcGetStab(targetId);
		}

        [Client]
        private void RpcGetStab(uint targetId) {
            EntityManager.Dispatcher(MessageID.HealthUpdate, targetId, UintBody.Default.Init(targetId));
        }

        

        public void HealthChange(float health) {
            if (isServer) {
                RpcHealthChange();
            } else {
                CmdHealthChange();
			}
            
		}

        [Command]
        public void CmdHealthChange() {

            Debug.Log(netId.Value + " cmd change health " + health);
            RpcHealthChange();
		}

        [ClientRpc]
        public void RpcHealthChange() {

            Debug.Log(netId.Value + " rpc change health " + health);
            healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        }

        [ClientRpc]
        public void RpcDied() {
            Debug.Log("died");
		}

        [ClientRpc]
        public void RpcGetHurt(Vector3 hurtPosition, Quaternion hurtRotation) {
            EffectManager.Instance.ShowEffect("player", hurtPosition, hurtRotation);
		}

    }
}