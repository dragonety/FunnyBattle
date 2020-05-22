using UnityEngine;

namespace Reconstitution {
    public class PlayerLocalMoveComponent : AbsComponet {

        private ObjectFeature objectFeature;

        private float deltaPosZ;
        private float deltaRotY;

        public override void OnInit() {
            objectFeature = Entity.GetFeature<ObjectFeature>();

            RegisterUpdate(OnUpdate);
            RegisterFixedUpdate(OnFixedUpdate);
        }

        private void OnUpdate(float deltaTime) {
            deltaPosZ = Input.GetAxis("Vertical") * Consts.moveSpeed * deltaTime;
            deltaRotY = Input.GetAxis("Horizontal") * Consts.rotateSpeed * deltaTime;
            // 同步
            
        }

        private void OnFixedUpdate(float deltaTime) {
            objectFeature.transform.Translate(0, 0, deltaPosZ);
            objectFeature.transform.Rotate(0, deltaRotY, 0);
            Debug.Log("player local move component fixedupdate " + deltaPosZ);
        }



    }
}