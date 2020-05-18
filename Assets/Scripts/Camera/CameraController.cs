using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [HideInInspector]
    public Transform transPlayer;

    [SerializeField]
    private Transform pivix;

    private void LateUpdate() {
        if (transPlayer != null) {
            pivix.position = transPlayer.position;
            pivix.rotation = transPlayer.rotation;
        }
    }

}
