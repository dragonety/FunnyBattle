using System.Collections;
using UnityEngine;
using UnityEngine;
using UnityEngine.Networking;

public class SwordController : MonoBehaviour {

    [SerializeField]
    private GameObject owner;

    [SerializeField]
    private int damage;

    private void OnTriggerEnter(Collider other) {
        if (CheckHitable(other)) {
            Hit(other.ClosestPointOnBounds(transform.position), other);
        }
    }

    private bool CheckHitable(Collider other) {
        if (other.tag == "Player") {
            PlayerController playerController = owner.GetComponent<PlayerController>();
            if (owner == other.gameObject) {
                return false;
			} else {
                Debug.Log("return attack" + playerController.isAttacking + playerController.health);
                return playerController.isAttacking;
            }
        }
        return false;
    }

    private void Hit(Vector3 hitPoint, Collider other) {
        if (other.tag == "Player") {
            other.gameObject.GetComponent<PlayerController>().GetStab(damage);
        }
    }
}