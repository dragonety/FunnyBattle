using System.Numerics;

namespace Reconstitution {
    public class QuaternionBody : IBody {

        public Quaternion value;

        public QuaternionBody Init(Quaternion value) {
            this.value = value;
            return this;
        }

        public static QuaternionBody Default = new QuaternionBody();
    }
}