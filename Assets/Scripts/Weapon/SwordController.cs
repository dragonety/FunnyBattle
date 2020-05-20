using BattleEvent;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class SwordController : MonoBehaviour {

    [SerializeField]
    private GameObject owner;

    [SerializeField]
    private int damage;

    private ReceiverSword receiverSword = new ReceiverSword();

    private void OnEnable() {
        receiverSword.RegisterDelegates();
    }

    private void OnDisable() {
        receiverSword.UnRegisterDelegates();
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("trigger enter");
        if (CheckHitable(other) && receiverSword.canAttack) {
            receiverSword.canAttack = false;
            Hit(other.ClosestPointOnBounds(transform.position), other);
        }
    }

    private bool CheckHitable(Collider other) {
        if (other.tag == "Player") {
            PlayerEntity playerController = owner.GetComponent<PlayerEntity>();
            if (owner == other.gameObject) {
                return false;
			} else {
                return true;
            }
        }
        return false;
    }

    private void Hit(Vector3 hitPoint, Collider other) {
        if (other.tag == "Player") {
            //EventManager.Instance.SendEvent(BattleEvent.EventType.hitAttack, other.gameObject.GetComponent<PlayerEntity>().netId.Value, damage);
            //CmdHit(other.gameObject.GetComponent<PlayerEntity>().netId.Value, damage);
            owner.GetComponent<PlayerAttack>().CmdAttack(other.gameObject.GetComponent<PlayerEntity>().netId.Value, damage);
        }
    }

}

public class ReceiverSword {

    //private SwordController sword;
    public bool canAttack = false;

    public void RegisterDelegates() {
        EventManager.Instance.RegisterEvent(BattleEvent.EventType.attack, OnEventProcessAttack);
    }

    public void UnRegisterDelegates() {
        EventManager.Instance.UnRegisterEvent(BattleEvent.EventType.attack);
    }

    private void OnEventProcessAttack(BaseEventMsg msg) {
        Debug.Log("onEvent attack");
        canAttack = true;
    }
}