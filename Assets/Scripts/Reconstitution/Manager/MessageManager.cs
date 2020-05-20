namespace Reconstitution {
    public class MessageManager {

        private static MessageRegister register;

        public static void Init() {
            register = new MessageRegister();
        }

        public static void Clear() {
            register.Dispose();
        }

        public static void Dispose() {
            register.Dispose();
            register = null;
        }

        public static void Dispatcher(int id, IBody body = null) {
            register.Dispatcher(id, body);
        }

        public static void RegisterMessage(int id, MessageDelegate messageDelegate) {
            register.Register(id, messageDelegate);
        }

        public static void UnregisterMessage(int id, MessageDelegate messageDelegate) {
            register.Unregister(id, messageDelegate);
        }

    }
}