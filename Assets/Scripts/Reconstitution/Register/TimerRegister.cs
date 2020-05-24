using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reconstitution {
    public class TimerRegister : IUpdate, IDisposable {

        private static int id = 1;

        private bool isInitialize;

        private bool debug = false;

        //  需要remove的timerTick
        private List<TimerTick> temp;
        //  遍历的timerTick
        private List<TimerTick> list;
        //  需要add的timerTick
        private List<TimerTick> adds;

        public TimerRegister() {
            temp = new List<TimerTick>();
            list = new List<TimerTick>();
            adds = new List<TimerTick>();

            OnInit();

        }

        public void OnInit() {
            Clear();
            isInitialize = true;
        }

        public void Dispose() {
            Clear();
            isInitialize = false;
        }

        public void Clear() {
            temp.Clear();
            list.Clear();
            adds.Clear();
        }

        public void OnUpdate(float deltaTime) {
            if (debug) Debug.Log("timer register update" + isInitialize + " temp " + temp.Count + " list " + list.Count + " add " + adds.Count );
            
            if (isInitialize) {
                //  先把注册获得的添加到List里面
                if (adds.Count > 0) {
                    list.AddRange(adds);
                    adds.Clear();
                }

                if (list.Count > 0 && deltaTime > 0) {
                    foreach (TimerTick tick in list) {
                        // 如果其他地方标记了需要移除，放到需要移除的列表里
                        if (tick.remove) {
                            temp.Add(tick);
                        } else {
                            //  每帧调用
                            if (tick.frame != null) {
                                tick.frame.Invoke();
                            } else {
                                tick.time -= deltaTime;
                                if (tick.time <= 0) {
                                    tick.func?.Invoke();
                                    tick.count -= 1;
                                    if (tick.count <= 0) {
                                        //  计数归零
                                        temp.Add(tick);
                                        tick.finish?.Invoke();
                                    } else {
                                        //  按照间隔时间重新开始
                                        tick.time = tick.interval;
                                    }
                                }
                            }
                        }
                    }
                    if (temp.Count > 0) {
                        foreach (TimerTick tick in temp) {
                            list.Remove(tick);
                        }
                        temp.Clear();
                    }
                }
            }
        }

        //  延迟delay执行一次func
        public int Register(float delay, Action func) {
            if (isInitialize) {
                return Register(delay, delay, 1, func, null);
            }
            return -1;
        }

        //  基本注册类型
        public int Register(float delay, float interval, int count, Action func, Action finish = null) {
            if (isInitialize && func != null) {
                TimerTick tick = new TimerTick();
                tick.id = id++;
                tick.time = delay;
                //  防止小于0
                tick.count = count < 0 ? int.MaxValue : count;
                tick.interval = interval;
                tick.func = func;
                tick.frame = null;
                tick.finish = finish;
                tick.remove = false;
                adds.Add(tick);

                if (debug) Debug.Log("timer register success" + tick.id);
                return tick.id;
            }
            return -1;
        }

        public void Unregister(int id) {
            if (isInitialize) {
                TimerTick tick = Try(id);
                if (tick != null) {
                    tick.remove = true;
                }
            }
        }

        private TimerTick Try(int id) {
            if (isInitialize) {
                foreach (TimerTick tick in list) {
                    if (tick.id == id) {
                        return tick;
                    }
                }
            }
            return null;
        }
    }
}