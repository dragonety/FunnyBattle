namespace Reconstitution {
    public class GameBehaviour : AbsBehaviour {
        public override void OnInit() {
            Add<GameBattleModule>();
        }
    }
}