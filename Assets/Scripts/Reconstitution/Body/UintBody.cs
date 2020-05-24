namespace Reconstitution {
    public class UintBody : IBody {

        public uint value;

        public UintBody Init(uint value) {
            this.value = value;
            return this;
        }

        public static UintBody Default = new UintBody();
    }
}