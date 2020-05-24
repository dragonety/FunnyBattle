using UnityEngine.Networking;
using UnityEngine;

namespace Reconstitution {
    public class PlayerTransform : NetworkBehaviour {

        //[SyncVar(hook = "OnPositionChange")]
        private Vector3 position;
        //[SyncVar(hook = "OnRotationChange")]
        private Quaternion rotation;

        [Client]
        public void SyncPosition(Vector3 position) {
            CmdSyncPosition(position);
		}

        [Command]
        private void CmdSyncPosition(Vector3 position) {
            this.position = position;
            //OnPositionChange(position);
            RpcSyncPosition(position);
		}

        [ClientRpc]
        private void RpcSyncPosition(Vector3 position) {
            this.position = position;
            OnPositionChange(position);
        }

        [Client]
        public void SyncRotation(Quaternion rotation) {
            CmdSyncRotation(rotation);
		}

        [Command]
        private void CmdSyncRotation(Quaternion rotation) {
            this.rotation = rotation;
            //OnRotationChange(rotation);
            RpcSyncRotation(rotation);
		}

        [ClientRpc]
        private void RpcSyncRotation(Quaternion rotation) {
            this.rotation = rotation;
            OnRotationChange(rotation);
        }

        [Client]
        private void OnPositionChange(Vector3 position) {
            EntityManager.Dispatcher(MessageID.PositionUpdate, netId.Value, Vector3Body.Default.Init(position));
        }

        [Client]
        private void OnRotationChange(Quaternion rotation) {
            EntityManager.Dispatcher(MessageID.RotationUpdate, netId.Value, QuaternionBody.Default.Init(rotation));
		}

    }
}