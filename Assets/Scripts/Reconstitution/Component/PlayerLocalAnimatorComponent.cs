using UnityEngine;

namespace Reconstitution {
    public class PlayerLocalAnimatorComponent : AbsComponet {

        private AnimatorFeature animatorFeature;

        public override void OnInit() {
            animatorFeature = Entity.GetFeature<AnimatorFeature>();

            RegisterUpdate(OnFixedUpdate);
        }

        private void OnFixedUpdate(float deltaTime) {
            if (Mathf.Abs(Input.GetAxis("Vertical")) > Consts.zero || Mathf.Abs(Input.GetAxis("Horizontal")) > Consts.zero) {
                animatorFeature.Move();
            } else {
                animatorFeature.Stop();
            }
            if (Input.GetMouseButtonDown(0)) {
                animatorFeature.Attack();
            }
            if (Input.GetMouseButtonDown(1)) {
                animatorFeature.Block();
            }
            if (Input.GetKeyDown(KeyCode.E)) {
                animatorFeature.Spawn();
            }
        }

    }
}