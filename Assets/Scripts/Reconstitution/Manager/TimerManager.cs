using System;

namespace Reconstitution {
    public class TimerManager {

        private static TimerRegister register;

        public static void Init() {
            register = new TimerRegister();
            //  TimerManager需要使用update,这里要在updateManager里注册timerManager的update事件
            UpdateManager.RegisterUpdate(OnUpdate);
        }

        public static void Dispose() {
            UpdateManager.UnregisterUpdate(OnUpdate);
            register.Dispose();
            register = null;
        }

        public static int Register(float delay, Action func) {
            return register.Register(delay, func);
        }

        public static void Unregister(int id) {
            register.Unregister(id);
        }

        public static void OnUpdate(float deltaTime) {
            register.OnUpdate(deltaTime);
        }

    }
}