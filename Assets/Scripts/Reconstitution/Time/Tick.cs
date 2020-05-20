namespace Reconstitution {
    public class Tick {

        private float curTime;
        private float limitTime;

        public Tick(float curTime) {
            this.curTime = curTime;
            this.limitTime = curTime;
        }

        public bool Ready(float deltaTime) {
            curTime += deltaTime;
            if (curTime >= limitTime) {
                return true;
            } else {
                return false;
            }
        }

        public void Reset() {
            curTime = 0;
        }

    }
}