using UnityEngine;
using UnityEditor;

public class EffectManager : SingletonMonobehaviour<EffectManager> {

    [SerializeField]
    GameObject defaultEffect;
    [SerializeField]
    GameObject bloodEffect;

    public void ShowEffect(GameObject effect, Vector3 position, Quaternion rotation) {
        Instantiate(effect, position, rotation, transform);
    }
    
    public void ShowEffect(string tag, Vector3 position, Quaternion rotation) {
        switch (tag) {
            case "Player":
            case "player":
                Instantiate(bloodEffect, position, rotation, transform);
                break;
            case "default":
            case "Default":
            default:
                Instantiate(defaultEffect, position, rotation, transform);
                break;
        }
    }
}