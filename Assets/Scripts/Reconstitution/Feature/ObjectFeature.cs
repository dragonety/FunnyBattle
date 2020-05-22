using UnityEngine;

namespace Reconstitution {

    public class ObjectFeature : AbsFeature {

        public GameObject gameObject { get; }
        public Transform transform { get; }

        public Vector3 position {
            get { return transform.position; }
            set { transform.position = value; }
        }

        public Quaternion rotation {
            get { return transform.rotation; }
            set { transform.rotation = value; }
        }

        private Rigidbody rigidBody;

        public ObjectFeature(GameObject gameObject) {
            this.gameObject = gameObject;
            this.transform = gameObject.transform;
        }

        public override void OnInit(IEntity entity) {
            base.OnInit(entity);
            rigidBody = GetComponent<Rigidbody>();
        }

        public T GetComponent<T>() where T : Component{
            return gameObject.GetComponent<T>();
        }

        public T AddComponent<T>() where T : Component {
            T t = GetComponent<T>();
            if (t == null) {
                return gameObject.AddComponent<T>();
            }
            return t;
        }

    }

}