using UnityEngine.Networking;

namespace Reconstitution {
    public class PlayerObject : NetworkBehaviour {

        [SyncVar]
        public float health;

        public override void OnStartServer() {
            
        }

    }
}