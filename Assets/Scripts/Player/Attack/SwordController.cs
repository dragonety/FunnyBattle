using UnityEngine;
using UnityEditor;

public class SwordController : ScriptableObject {
    [MenuItem("Tools/MyTool/Do It in C#")]
    static void DoIt() {
        EditorUtility.DisplayDialog("MyTool", "Do It in C# !", "OK", "");
    }
}