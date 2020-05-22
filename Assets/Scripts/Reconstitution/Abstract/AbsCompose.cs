using UnityEngine;

namespace Reconstitution {
    public abstract class AbsCompose : ICompose {

        private bool debug = false;

        public virtual void OnInit() {
        }

        public virtual void OnRemove() {
        }

        public virtual void OnUpdate(float deltaTime) {
            if (debug) Debug.Log("AbsCompose OnUpdate");
        }

        public virtual void OnFixedUpdate(float deltaTime) {
            if (debug) Debug.Log("AbsCompose OnFixedUpdate");
        }
    }
}