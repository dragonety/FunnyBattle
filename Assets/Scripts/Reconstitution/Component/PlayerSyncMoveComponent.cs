using UnityEngine;

namespace Reconstitution {
    public class PlayerSyncMoveComponent : AbsComponet {

        private ObjectFeature objectFeature;

        private Vector3 position;
        private Quaternion rotation;

        private bool debug = false;

        public override void OnInit() {

            objectFeature = Entity.GetFeature<ObjectFeature>();

            position = objectFeature.position;
            rotation = objectFeature.rotation;
            
            RegisterMessage(MessageID.PositionUpdate, (IBody body) => {
                if (debug) Debug.Log("message position update");
                position = (body as Vector3Body).value;
            });

            RegisterMessage(MessageID.RotationUpdate, (IBody body) => {
                if (debug) Debug.Log("message rotation update");
                rotation = (body as QuaternionBody).value;
            });
            
            RegisterFixedUpdate(OnFixedUpdate);
        }

        private void OnFixedUpdate(float deltaTime) {
            objectFeature.position = Vector3.Lerp(objectFeature.position, position, deltaTime * 10);
            objectFeature.rotation = Quaternion.Slerp(objectFeature.rotation, rotation, deltaTime * 10);
        }

    }
}