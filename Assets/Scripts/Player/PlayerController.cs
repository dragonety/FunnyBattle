using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Animator))]
public class PlayerController : NetworkBehaviour {

    private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;
    private Animator animator;

    #region SyncValue

    private static Vector3 vectFlat = new Vector3(1, 0, 1);

    [SyncVar(hook = "SyncPositionValues")]
    private Vector3 syncPos;

    [SyncVar(hook = "SyncRotationValues")]
    private Quaternion syncRot;

    [SerializeField]
    private float lerpRate = 10;

    [SerializeField]
    [SyncVar(hook = "SyncHealth")]
    private int health = 100;

    [SerializeField]
    private RectTransform healthBar;

    #endregion

    #region Movement

    [SerializeField]
    private float moveSpeed = 5;
    [SerializeField]
    private float rotateSpeed = 30;

	#endregion

	#region Animation
	
    [SerializeField]
    private float groundCheckDistance = 0.1f;

    private bool isDeath;
    private bool isMoving;
    private bool isJumping;
    private bool isBlocking;
    private bool isAttacking;

	#endregion

	#region skill

	[SerializeField]
    float shootSpeed = 40;
    [SerializeField]
    GameObject magicBall;
    [SerializeField]
    Transform shootPoint;

	#endregion

    private void Start() {
        if (isLocalPlayer) {
            Camera.main.GetComponent<CameraController>().transPlayer = transform;
        }

        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();

        isDeath = false;
    }

    void Update() {
        if (isLocalPlayer) {
            CheckMove();
            CheckAttack();
        } else {
            LerpSyncData();
        }        
    }

    private void FixedUpdate() {
        TransmitData();
    }

    private void CheckMove() {

        if (isDeath) {
            return;
        }

        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        var move = hor * transform.right + ver * transform.forward;

        if (move.magnitude > 1) {
            move.Normalize();
        }

        move = transform.InverseTransformDirection(move);

        GetMove(move);

    }

    private void CheckAttack() {

        if (Input.GetMouseButtonDown(0)) {
            Attack();
        }

        if (Input.GetMouseButtonDown(1)) {
            Block();
        }
    }

    private void LerpSyncData() {
        OrdinaryLerping();
    }

    [Client]
    private void TransmitData() {
        if (isLocalPlayer) {
            CmdProvidePosition(transform.position);
        }
        if (isLocalPlayer) {
            CmdProvideRotation(transform.rotation);
        }
    }

    [Command]
    private void CmdProvidePosition(Vector3 pos) {
        syncPos = pos;
    }

    [Command]
    private void CmdProvideRotation(Quaternion rot) {
        syncRot = rot;
    }

    [Client]
    public void SyncPositionValues(Vector3 lastPos) {
        syncPos = lastPos;
    }

    [Client]
    public void SyncRotationValues(Quaternion lastRot) {
        syncRot = lastRot;
    }

	[Client]
    private void SyncHealth(int health) {
        healthBar.sizeDelta = new Vector2(health, healthBar.sizeDelta.y);
        if (health <= 0) {
            animator.SetBool("isDeath", true);
        }
    }

    private void OrdinaryLerping() {
        transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime * lerpRate);
        transform.rotation = Quaternion.Lerp(transform.rotation, syncRot, Time.deltaTime * lerpRate);
    }

    private void GetMove(Vector3 move) {
        if (move.magnitude > 0) {
            if (!animator.GetBool("isMoving")) {
                animator.SetBool("isMoving", true);
            }

            transform.Rotate(0.0f, move.x * rotateSpeed * Time.deltaTime, 0.0f);
            transform.Translate(Vector3.forward * move.z * moveSpeed * Time.deltaTime);
        } else {
            if (animator.GetBool("isMoving")) {
                animator.SetBool("isMoving", false);
            }

        }
    }

    public void Attack() {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("Attack01")) {
            animator.Play("Attack01");
            CmdShootBall();
        }
    }

    public void GetHurt(float damage) {
        animator.Play("Damage02");
        health -= (int)damage;
    }

    public void GetStab(float damage) {
        animator.Play("Damage01");
        health -= (int)damage;
    }

    [Command]
    private void CmdShootBall() {
        var magicBall = Instantiate(this.magicBall, shootPoint.position, shootPoint.rotation);
        magicBall.GetComponent<MagicController>().velocity = magicBall.transform.forward * shootSpeed;
        NetworkServer.Spawn(magicBall);
        RpcInitBall(magicBall);
        Destroy(magicBall, 2.0f);
    }

	[ClientRpc]
	private void RpcInitBall(GameObject magicBall) {
        GameManager.Instance.AddSpawn(magicBall.GetComponent<MagicController>(), this);
    }

    public void Block() {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);
        if (!info.IsName("Attack02")) {
            animator.Play("Attack02");
        }
    }

}