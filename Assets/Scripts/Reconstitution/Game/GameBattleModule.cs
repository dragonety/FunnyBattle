namespace Reconstitution {
    public class GameBattleModule : AbsCompose {

        public override void OnInit() {
            UpdateManager.Init();
            MessageManager.Init();
            //timerManager
            //battleManager
        }

        public override void OnRemove() {
            //battleManager
            //timerManager
            MessageManager.Dispose();
            UpdateManager.Dispose();
        }

        public override void OnUpdate(float deltaTime) {
            UpdateManager.OnUpdate(deltaTime);
        }

        public override void OnFixedUpdate(float deltaTime) {
            UpdateManager.OnFixedUpdate(deltaTime);
        }
    }
}