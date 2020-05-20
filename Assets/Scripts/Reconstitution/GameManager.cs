using System.Collections.Generic;
using UnityEngine;

namespace Reconstitution {

    public class GameManager : Singleton<GameManager>, ILifecycle, IUpdate, IFixedUpdate {

        //  方便对所有compose遍历
        private List<ICompose> composeList;
        //  方便通过类型找到对应compose, 类似getcomponent
        private Dictionary<System.Type, ICompose> composeDict;

        public GameManager() {
            composeList = new List<ICompose>();
            composeDict = new Dictionary<System.Type, ICompose>();
        }

        //  Config

        public void OnInit() {
            // AudioManager
            // SystemBehaviour
        }

        public void OnRemove() {
            foreach(ICompose compose in composeList) {
                compose.OnRemove();
            }
            composeList.Clear();
            composeDict.Clear();
        }

        public void OnUpdate(float deltaTime) {
            foreach(ICompose compose in composeList) {
                compose.OnUpdate(deltaTime);
            }
        }

        public void OnFixedUpdate(float deltaTime) {
            foreach(ICompose compose in composeList) {
                compose.OnFixedUpdate(deltaTime);
            }
        }

        /*  需要约束T可以被new：,new()
         *  return default(T)
         *  Add的同时进行Init
         */
        public T Add<T>() where T : ICompose ,new(){
            System.Type type = typeof(T);
            if (!composeDict.ContainsKey(type)) {
                T compose = new T();
                composeList.Add(compose);
                composeDict.Add(type, compose);
                compose.OnInit();
                return compose;
            }
            return default(T);
        }

        /*  这里使用TryGetValue而不用ContainsKey,更方便之后对compose进行OnRemove
         *  Remove的同时组件的Remove也要被触发
         */
        public void Remove<T>() where T : ICompose {
            System.Type type = typeof(T);
            ICompose compose = null;
            if (composeDict.TryGetValue(type, out compose)) {
                composeDict.Remove(type);
                composeList.Remove(compose);
                compose.OnRemove();
            }
        }
    }

}