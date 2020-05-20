using UnityEngine;

namespace Reconstitution {

    public class Engine : MonoBehaviour {

        private void Awake() {
            // config
        }

        private void Start() {
            GameManager.Instance.OnInit();
        }

        private void Update() {
            GameManager.Instance.OnUpdate(Time.deltaTime);
        }

        private void FixedUpdate() {
            GameManager.Instance.OnFixedUpdate(Time.deltaTime);
        }

        private void OnDestroy() {
            GameManager.Instance.OnRemove();
        }

    }
}
