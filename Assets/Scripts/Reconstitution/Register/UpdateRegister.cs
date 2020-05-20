﻿using System;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;

namespace Reconstitution {
    public class UpdateRegister : IUpdate, IFixedUpdate, IDisposable {

        private List<UpdateDelegate> delegateUpdates;
        private List<UpdateDelegate> delegateTempUpdates;
        private List<UpdateDelegate> delegateFixedUpdates;
        private List<UpdateDelegate> delegateTempFixedUpdates;

        public UpdateRegister() {
            delegateUpdates = new List<UpdateDelegate>();
            delegateTempUpdates = new List<UpdateDelegate>();
            delegateFixedUpdates = new List<UpdateDelegate>();
            delegateTempFixedUpdates = new List<UpdateDelegate>();
        }

        public void OnInit() {
            Clear();
        }

        public void Dispose() {
            Clear();
        }

        public void Clear() {
            delegateUpdates.Clear();
            delegateTempUpdates.Clear();
            delegateFixedUpdates.Clear();
            delegateTempFixedUpdates.Clear();
        }

        /*  添加Temp的意义在于，防止在遍历update或fixedupdate的同时操作delegate造成的错误
         * 
         */
        public void OnUpdate(float deltaTime) {
            if (delegateUpdates.Count > 0) {
                delegateTempUpdates.Clear();
                delegateTempUpdates.AddRange(delegateUpdates);
                foreach (UpdateDelegate deleUpdate in delegateTempUpdates) {
                    deleUpdate(deltaTime);
                }
            }
        }

        public void OnFixedUpdate(float deltaTime) {
            if (delegateFixedUpdates.Count > 0) {
                delegateTempFixedUpdates.Clear();
                delegateTempFixedUpdates.AddRange(delegateTempFixedUpdates);
                foreach (UpdateDelegate deleUpdate in delegateTempFixedUpdates) {
                    deleUpdate(deltaTime);
                }
            }
        }

        public void RegisterUpdate(UpdateDelegate update) {
            if (!delegateUpdates.Contains(update)) {
                delegateUpdates.Add(update);
            }
        }

        public void UnregisterUpdate(UpdateDelegate update) {
            if (delegateUpdates.Contains(update)) {
                delegateUpdates.Remove(update);
            }
        }

        public void RegisterFixedUpdate(UpdateDelegate fixedUpdate) {
            if (!delegateFixedUpdates.Contains(fixedUpdate)) {
                delegateFixedUpdates.Add(fixedUpdate);
            }
        }

        public void UnregisterFixedUpdate(UpdateDelegate fixedUpdate) {
            if (delegateFixedUpdates.Contains(fixedUpdate)) {
                delegateFixedUpdates.Remove(fixedUpdate);
            }
        }

        
    }
}