using UnityEngine;

namespace Reconstitution {
    public class SwordCollision : MonoBehaviour {

        [HideInInspector]
        public uint playerId = 0;
        [HideInInspector]
        public bool canAttack = false;

        [SerializeField]
        private PlayerAttack player;

        private PlayerObject target;

		private void OnTriggerEnter(Collider collider) {
            if (collider.tag == "Player" && canAttack && !CheckSelf(collider.gameObject)) {
                Entity entity = EntityManager.GetEntity(target.netId.Value);
                if (entity != null) {
                    canAttack = false;
                    //EntityManager.Dispatcher(MessageID.PlayerHurt, player.netId.Value, FloatBody.Default.Init(Consts.weaponDamage));
                    Debug.Log(playerId + " trigger " + target.netId.Value);
                    //EntityManager.Dispatcher(MessageID.PlayerHurt, playerId, UintBody.Default.Init(target.netId.Value));
                    player.GetStab(target.netId.Value);
                }
            }
        }

        private bool CheckSelf(GameObject gameObject) {
            target = gameObject.GetComponent<PlayerObject>();
            if (playerId == target.netId.Value) {
                return true;
			}
            return false;
		}

    }
}