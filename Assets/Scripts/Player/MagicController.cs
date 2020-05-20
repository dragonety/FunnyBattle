using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

public class MagicController : NetworkBehaviour {

    [HideInInspector]
    [SyncVar]
    public Vector3 velocity;

    [SerializeField]
    private int damage = 10;

    [SerializeField]
    private float checkDistance = 0.5f;

    [SerializeField]
    private GameObject goHit;

    Rigidbody rigidbody;

    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void LateUpdate() {
        rigidbody.velocity = velocity;
    }

    private void OnTriggerEnter(Collider other) {
        if (!CheckSelf(other)) {
            Hit(other.ClosestPointOnBounds(transform.position), other);
        }
    }

    private bool CheckSelf(Collider other) {
		if (other.tag == "Player") {
            //PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            PlayerEntity playerController = other.gameObject.GetComponent<PlayerEntity>();
			if (playerController.netId == GameManager.Instance.GetPlayer(netId)) {
                return true;
			}
		}
        return false;
    }

    private void CheckCollision() {
        RaycastHit hitInfo;

#if UNITY_EDITOR
        Debug.DrawRay(transform.position, velocity, Color.red);
#endif

        if(Physics.Raycast(transform.position, velocity, out hitInfo, checkDistance)) {
            Hit(hitInfo.point, hitInfo.collider);
        }
    }

    private void Hit(Vector3 hitPoint, Collider other) {
        EffectManager.Instance.ShowEffect(goHit, hitPoint, transform.rotation);
        EffectManager.Instance.ShowEffect(other.tag, hitPoint, transform.rotation);

        if (other.tag == "Player") {
            other.gameObject.GetComponent<PlayerController>().GetHurt(damage);
            Debug.Log("hit");
        }

        Destroy(gameObject);
    }

}