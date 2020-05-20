using System.Numerics;

namespace Reconstitution {
    public class Vector3Body : IBody {

        public Vector3 value;

        public Vector3Body Init(Vector3 value) {
            this.value = value;
            return this;
        }

        public static Vector3Body Default = new Vector3Body();
    }
}