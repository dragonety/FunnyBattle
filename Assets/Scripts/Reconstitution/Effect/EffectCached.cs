using System;
using System.Collections.Generic;
using UnityEngine;

namespace Reconstitution {
    public class EffectCached : IDisposable {

        private string path;
        private Queue<Effect> queue;

        public EffectCached(string path) {
            this.path = path;
            queue = new Queue<Effect>();
        }

        public void Dispose() {
            foreach (Effect effect in queue) {
                UnityEngine.Object.Destroy(effect);
            }
            queue.Clear();
        }

        public Effect Create(Transform parent) {
            UnityEngine.Object prefab = Resources.Load(path);
            if (prefab != null) {
                GameObject effectGameObject = UnityEngine.Object.Instantiate(prefab) as GameObject;
                effectGameObject.name = prefab.name;
                ResetEffect(effectGameObject.transform, parent);
                Effect effect = effectGameObject.AddComponent<Effect>();
                effect.path = path;
                return effect;
            } else {
                Debug.LogError("can't find resource " + path);
                return null;
            }
        }

        public Effect Take(Transform parent) {
            Effect effect = null;
            if (queue.Count > 0) {
                effect = queue.Dequeue();
                effect.transform.SetParent(parent);
            } else {
                effect = Create(parent);
            }
            return effect;
        }

        public void Recycle (Effect effect, Transform parent) {
            ResetEffect(effect.transform, parent);
            effect.Stop();
            queue.Enqueue(effect);
        }

        private void ResetEffect(Transform effectTransform, Transform parent) {
            if (parent != null && effectTransform != null) {
                effectTransform.SetParent(parent);
            }
            effectTransform.localPosition = Vector3.zero;
            effectTransform.localRotation = Quaternion.identity;
            effectTransform.localScale = Vector3.one;
        }

        

    }
}