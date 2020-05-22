namespace Reconstitution {
    public class GameBattleModule : AbsCompose {

        public override void OnInit() {
            UpdateManager.Init();
            MessageManager.Init();
            TimerManager.Init();
            BattleManager.Init();
        }

        public override void OnRemove() {
            BattleManager.Dispose();
            TimerManager.Dispose();
            MessageManager.Dispose();
            UpdateManager.Dispose();
        }

        public override void OnUpdate(float deltaTime) {
            base.OnUpdate(deltaTime);
            UpdateManager.OnUpdate(deltaTime);
        }

        public override void OnFixedUpdate(float deltaTime) {
            base.OnFixedUpdate(deltaTime);
            UpdateManager.OnFixedUpdate(deltaTime);
        }
    }
}