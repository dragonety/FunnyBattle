using UnityEngine;
using UnityEngine.Networking;

public class PlayerEntity : NetworkBehaviour {

    [Header("=====Prefab=====")]
    [SerializeField]
    private GameObject magicBall;

    [Header("=====Child=====")]
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private GameObject sword;
    [SerializeField]
    private GameObject shield;
    [SerializeField]
    private RectTransform healthBar;

    [Header("=====Value=====")]
    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float rotateSpeed = 45;
    [SerializeField]
    private float shootSpeed = 40;
    [SerializeField]
    private int maxHealth = 100;
    [SerializeField]
    private float lerpRate = 20;

    [Header("=====Debug=====")]
    [SerializeField]
    private bool isDebug = true;
    [SerializeField]
    private bool isLoopBlock = false;

    [Header("=====Sync=====")]
    [SyncVar]
    private Vector3 position;
    [SyncVar]
    private Quaternion rotation;
    [SyncVar(hook = "OnHealthChange")]
    public int health;

    Player player = new Player();

    private void Start() {

        health = maxHealth;

        player.gameObject = gameObject;
        player.transform = transform;
        player.magicBall = magicBall;
        player.shootPoint = shootPoint;
        player.sword = sword;
        player.shield = shield;
        player.health = health;
        player.lerpRate = lerpRate;
        player.healthBar = healthBar;
        player.moveSpeed = moveSpeed;
        player.rotateSpeed = rotateSpeed;
        player.shootSpeed = shootSpeed;
        player.isDebug = isDebug;
        player.isLoopBlock = isLoopBlock;
        player.isLocal = isLocalPlayer;
        player.position = position;
        player.rotation = rotation;
        player.id = netId.Value;
        player.Start();

        if (isLocalPlayer) {
            Camera.main.GetComponent<CameraController>().transPlayer = transform;
            GameManager.Instance.localPlayer = gameObject;
        }

    }

    private void Update() {
        player.health = health;
        player.position = position;
        player.rotation = rotation;
        player.Update(Time.deltaTime);
    }

    private void LateUpdate() {
        if (isLocalPlayer) {
            CmdOnPositionChange(transform.position);
            CmdOnRotationChange(transform.rotation);
        }
    }

    private void OnHealthChange(int lastHealth) {
        healthBar.sizeDelta = new Vector2(lastHealth, healthBar.sizeDelta.y);
    }

    [Command]
    private void CmdOnPositionChange(Vector3 lastPosition) {
        position = lastPosition;
        //player.position = position;
    }

    [Command]
    private void CmdOnRotationChange(Quaternion lastRotation) {
        rotation = lastRotation;
        //player.rotation = rotation;
    }


    [Command]
    public void CmdSpawnGameObject(GameObject gameObject) {
        Debug.Log("server control spawn");
        RpcSpawnGameObject(gameObject);
    }

    [ClientRpc]
    public void RpcSpawnGameObject(GameObject gameObject) {
        Debug.Log("client spawn gameObject");
        GameObject.Instantiate(gameObject);
    }

}