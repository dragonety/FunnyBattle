using System.Numerics;

namespace Reconstitution {
    public class TransformBody : IBody {

        public Vector3 position;
        public Quaternion rotation;

        public TransformBody Init(Vector3 position, Quaternion rotation) {
            this.position = position;
            this.rotation = rotation;
            return this;
        }

        public static TransformBody Default = new TransformBody();
    }
}