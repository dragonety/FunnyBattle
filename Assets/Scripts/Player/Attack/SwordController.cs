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
        if (!CheckSelf(other)) {
            Hit(other.ClosestPointOnBounds(transform.position), other);
        }
    }

    private bool CheckSelf(Collider other) {
        if (other.tag == "Player") {
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            if (owner == other.gameObject) {
                return true;
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