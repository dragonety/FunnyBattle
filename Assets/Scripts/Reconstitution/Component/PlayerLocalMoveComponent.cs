using UnityEngine;

namespace Reconstitution {
    public class PlayerLocalMoveComponent : AbsComponet {

        private ObjectFeature objectFeature;
        private PlayerTransform playerTransform;

        private Vector3 lastPosition;
        private Quaternion lastRotation;

        private float deltaPosZ;
        private float deltaRotY;

        public override void OnInit() {
            objectFeature = Entity.GetFeature<ObjectFeature>();
            playerTransform = objectFeature.GetComponent<PlayerTransform>();

            RegisterUpdate(OnUpdate);
            RegisterFixedUpdate(OnFixedUpdate);
            // 初始化Last
            lastPosition = objectFeature.transform.position;
            lastRotation = objectFeature.transform.rotation;
        }

        private void OnUpdate(float deltaTime) {
            deltaPosZ = Input.GetAxis("Vertical") * Consts.moveSpeed * deltaTime;
            deltaRotY = Input.GetAxis("Horizontal") * Consts.rotateSpeed * deltaTime;
            // 同步
            Vector3 position = objectFeature.transform.position;
            Quaternion rotation = objectFeature.transform.rotation;
            // 几乎没变到时候不发送同步信息
            
            if (Vector3.Distance(position, lastPosition) > Consts.zero) {
                lastPosition = objectFeature.transform.position;
                playerTransform.SyncPosition(position);
			}
            if (Quaternion.Angle(rotation, lastRotation) > Consts.zero) {
                lastRotation = objectFeature.transform.rotation;
                playerTransform.SyncRotation(rotation);
			}
            
        }

        private void OnFixedUpdate(float deltaTime) {
            objectFeature.transform.Translate(0, 0, deltaPosZ);
            objectFeature.transform.Rotate(0, deltaRotY, 0);
            //Debug.Log("player local move component fixedupdate " + deltaPosZ);
        }



    }
}