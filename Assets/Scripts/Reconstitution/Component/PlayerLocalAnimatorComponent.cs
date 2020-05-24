using UnityEngine;

namespace Reconstitution {
    public class PlayerLocalAnimatorComponent : AbsComponet {

        private AnimatorFeature animatorFeature;
        private PlayerAttack playerAttack;

        private bool isSpecialMove;

        private bool debug = true;

        public override void OnInit() {
            animatorFeature = Entity.GetFeature<AnimatorFeature>();
            playerAttack = Entity.GetFeature<ObjectFeature>().gameObject.GetComponent<PlayerAttack>();

            isSpecialMove = false;

            RegisterFixedUpdate(OnFixedUpdate);
        }

        private void OnFixedUpdate(float deltaTime) {
            if (Mathf.Abs(Input.GetAxis("Vertical")) > Consts.zero || Mathf.Abs(Input.GetAxis("Horizontal")) > Consts.zero) {
                animatorFeature.Move();
            } else {
                animatorFeature.Stop();
            }
            if (Input.GetMouseButtonDown(0) && !isSpecialMove) {
                isSpecialMove = true;
                TimerManager.Register(Consts.attackTime, OnSpecialMoveOver);
                animatorFeature.Attack();
                playerAttack.Attack();
                //MessageManager.Dispatcher(MessageID.PlayerAttack);
            }
            if (Input.GetMouseButtonDown(1) && !isSpecialMove) {
                isSpecialMove = true;
                TimerManager.Register(Consts.blockTime, OnSpecialMoveOver);
                animatorFeature.Block();
                playerAttack.Block();
                //MessageManager.Dispatcher(MessageID.PlayerBlock);
            }
            if (Input.GetKeyDown(KeyCode.E) && !isSpecialMove) {
                isSpecialMove = true;
                TimerManager.Register(Consts.spawnTIme, OnSpecialMoveOver);
                animatorFeature.Spawn();
                playerAttack.Spawn();
                //MessageManager.Dispatcher(MessageID.PlayerSpawn);
            }
        }

        private void OnSpecialMoveOver() {
            isSpecialMove = false;
            if (debug) Debug.Log("special move over");
		}

    }
}