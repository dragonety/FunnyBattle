using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

public class MagicEntity : NetworkBehaviour {

    [Header("=====Value=====")]
    [SerializeField]
    private float moveSpeed = 40;

    [Header("=====Sync=====")]
    [SyncVar]
    private Vector3 velocity;

    Magic magic = new Magic();

    private void Start() {
        magic.Start();
    }

    private void Update() {
        magic.Update(Time.deltaTime);
    }
}