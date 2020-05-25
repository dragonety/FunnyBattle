using BattleEvent;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SwordController : MonoBehaviour {

    [SerializeField]
    private PlayerEntity owner;

    [SerializeField]
    private int damage;

    private bool debug = false;

    private ReceiverSword receiverSword = new ReceiverSword();

    private void OnEnable() {
        receiverSword.RegisterDelegates();
    }

    private void OnDisable() {
        receiverSword.UnRegisterDelegates();
    }

    private void OnTriggerEnter(Collider other) {
        if (debug) Debug.Log("trigger enter " + receiverSword.canAttack + other.tag);
        if (CheckHitable(other) && receiverSword.canAttack) {
            if (debug) Debug.Log("trigger enter success");
            receiverSword.canAttack = false;
            Hit(other.ClosestPointOnBounds(transform.position), other);
        }
    }

    private bool CheckHitable(Collider other) {
        if (other.tag == "Player") {
            PlayerEntity otherEntity = other.gameObject.GetComponent<PlayerEntity>();
            if (debug) Debug.Log(owner.netId + "hit" + otherEntity.netId);
            if (owner.netId == otherEntity.netId) {
                return false;
			} else {
                return true;
            }
        }
        return false;
    }

    private void Hit(Vector3 hitPoint, Collider other) {
        owner.Attack(other.gameObject.GetComponent<PlayerEntity>().netId.Value, damage, hitPoint);
    }

}

public class ReceiverSword {

    private bool debug = false;

    //private SwordController sword;
    public bool canAttack = false;

    public void RegisterDelegates() {
        EventManager.Instance.RegisterEvent(BattleEvent.EventType.attack, OnEventProcessAttack);
    }

    public void UnRegisterDelegates() {
        EventManager.Instance.UnRegisterEvent(BattleEvent.EventType.attack);
    }

    private void OnEventProcessAttack(BaseEventMsg msg) {
        if (debug) Debug.Log("onEvent attack");
        canAttack = true;
    }
}