using System;
using System.Collections.Generic;

namespace Reconstitution {
    public class Features : IDisposable {

        private List<IFeature> featureList;
        private Dictionary<Type, IFeature> featureDict;

        public Features() {
            featureList = new List<IFeature>();
            featureDict = new Dictionary<Type, IFeature>();
        }

        public void OnInit() {
            Dispose();
        }

        /*  feature不同于attribute 和 config需要额外dispose, add的时候也需要额外加init
         * 
         */
        public void Dispose() {
            foreach(IFeature feature in featureList) {
                feature.OnRemove();
            }
            featureList.Clear();
            featureDict.Clear();
        }

        public T AddFeature<T>(T feature, IEntity entity) where T : IFeature {
            Type type = feature.GetType();
            if (!featureDict.ContainsKey(type)) {
                featureList.Add(feature);
                featureDict.Add(type, feature);
                feature.OnInit(entity);
                return (T)feature;
            }
            return default(T);
        }

        public T GetFeature<T>() where T : IFeature {
            IFeature feature = default(T);
            featureDict.TryGetValue(typeof(T), out feature);
            return (T)feature;
        }

    }
}