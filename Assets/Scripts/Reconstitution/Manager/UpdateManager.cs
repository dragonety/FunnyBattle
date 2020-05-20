namespace Reconstitution {
    public class UpdateManager {

        private static UpdateRegister register;

        public static void Init() {
            register = new UpdateRegister();
        }

        public static void Clear() {
            register.Dispose();
        }

        public static void Dispose() {
            register.Dispose();
            register = null;
        }

        public static void OnUpdate(float deltaTime) {
            register.OnUpdate(deltaTime);
        }

        public static void OnFixedUpdate(float deltaTime) {
            register.OnFixedUpdate(deltaTime);
        }

        public static void RegisterUpdate(UpdateDelegate update) {
            register.RegisterUpdate(update);
        }

        public static void RegisterFixedUpdate(UpdateDelegate fixedUpdate) {
            register.RegisterFixedUpdate(fixedUpdate);
        }

        public static void UnregisterUpdate(UpdateDelegate update) {
            register.UnregisterUpdate(update);
        }

        public static void UnregisterFixedUpdate(UpdateDelegate fixedUpdate) {
            register.UnregisterUpdate(fixedUpdate);
        }

    }
}