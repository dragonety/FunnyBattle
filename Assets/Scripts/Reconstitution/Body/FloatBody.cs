namespace Reconstitution {
    public class FloatBody : IBody {

        public float value;

        public FloatBody Init(float value) {
            this.value = value;
            return this;
        }

        public static FloatBody Default = new FloatBody();
    }
}