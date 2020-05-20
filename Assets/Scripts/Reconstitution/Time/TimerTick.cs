using System;

namespace Reconstitution {
    public class TimerTick {

        public int id;
        public int count;
        public float time;
        public float interval;
        public Action func;
        public Action frame;
        public Action finish;
        public bool remove;

        public void Reset() {
            id = 0;
            count = 0;
            interval = 0;
            func = null;
            frame = null;
            finish = null;
            remove = false;
        }
    }
}