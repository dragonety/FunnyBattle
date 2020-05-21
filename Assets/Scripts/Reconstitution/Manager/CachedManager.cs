using UnityEngine;
using UnityEditor;
using Reconstitution;

public class CachedManager {

    public static EffectCachedPool effectCachedPool;

    public static void Init() {
        effectCachedPool = new EffectCachedPool();
        effectCachedPool.OnInit();
    }

    public static void Dispose() {
        effectCachedPool.OnRemove();
        effectCachedPool = null;
    }

}