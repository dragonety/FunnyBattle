using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Reconstitution {

    public class EffectCachedPool : ILifecycle {

        private GameObject root;
        private Dictionary<string, EffectCached> cacheds;

        public void OnInit() {
            root = new GameObject("EffectCachedPool");
            root.transform.position = Vector3.one * 9999;
            cacheds = new Dictionary<string, EffectCached>();
        }

        public void OnRemove() {
            foreach (EffectCached cached in cacheds.Values) {
                cached.Dispose();
            }
            cacheds.Clear();
            cacheds = null;
            UnityEngine.Object.Destroy(root);
            root = null;
        }

        public Effect Take(string path, Transform parent) {
            EffectCached effectCached = null;
            if (!cacheds.TryGetValue(path, out effectCached)) {
                effectCached = new EffectCached(path);
                cacheds.Add(path, effectCached);
            }
            return effectCached.Take(parent);
        }

        public void Recycle(Effect effect) {
            if (effect != null) {
                EffectCached effectCached = null;
                if (cacheds.TryGetValue(effect.path, out effectCached)) {
                    effectCached.Recycle(effect, root.transform);
                }
            }
        }
    }
}