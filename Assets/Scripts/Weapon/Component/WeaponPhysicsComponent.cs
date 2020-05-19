using UnityEngine;
using UnityEditor;

public class WeaponPhysicsComponent : ScriptableObject {
    [MenuItem("Tools/MyTool/Do It in C#")]
    static void DoIt() {
        EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
    }
}