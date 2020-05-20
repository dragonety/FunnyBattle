using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;
using BattleEvent;

public class PlayerAttack : NetworkBehaviour {

    [Command]
    public void CmdAttack(uint id, int damage) {
        Debug.Log("cmd attack " + id + " damage " + damage);
        RpcGetAttack(id, damage);
        //EventManager.Instance.SendEvent(BattleEvent.EventType.hitAttack, id, damage);
    }

    [ClientRpc]
    public void RpcGetAttack(uint id, int damage) {
        EventManager.Instance.SendEvent(BattleEvent.EventType.hitAttack, id, damage);
    }
}