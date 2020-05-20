namespace Reconstitution {
    public class IntBody : IBody {

        public int value;

        public IntBody Init(int value) {
            this.value = value;
            return this;
        }

        public static IntBody Default = new IntBody();
    }
}