using UnityEngine.Networking;
using UnityEngine;

namespace Reconstitution {
    public class PlayerAttack : NetworkBehaviour {

        [SerializeField]
        private GameObject sword;

        private void Start() {
            EntityManager.AddEntity(EntityFactory.CreateSword(netId.Value + Consts.weaponIDStart, GroupID.sword, sword, isLocalPlayer));
        }

        private void OnDestroy() {
            EntityManager.RemoveEntity(netId.Value + Consts.weaponIDStart);
        }

        public void Attack() {
            CmdAttack();
		}

        [Command]
        private void CmdAttack() {
            RpcAttack();
		}

        [ClientRpc]
        private void RpcAttack() {
            EntityManager.Dispatcher(MessageID.PlayerAttack, netId.Value + Consts.weaponIDStart, UintBody.Default.Init(netId.Value));
        }

        public void GetStab(uint targetId) {
            Debug.Log(netId.Value + " get stab " + targetId);
            CmdGetStab(targetId);
        }

        [Command]
        private void CmdGetStab(uint targetId) {
            Debug.Log(netId.Value + " cmd get stab " + targetId);
            RpcGetStab(targetId);
        }

        [ClientRpc]
        private void RpcGetStab(uint targetId) {
            Debug.Log(netId.Value + " rpc get stab " + targetId);
            EntityManager.Dispatcher(MessageID.HealthUpdate, targetId, FloatBody.Default.Init(Consts.weaponDamage));
        }

        public void Block() {

		}

        public void Spawn() {

		}

    }
}