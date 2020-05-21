namespace Reconstitution {
    public class BattleManager {
        public static void Init() {
            CachedManager.Init();
            EntityManager.Init();
        }

        public static void Dispose() {
            EntityManager.Dispose();
            CachedManager.Dispose();
        }
    }
}