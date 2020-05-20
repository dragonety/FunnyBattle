using UnityEngine;
using UnityEditor;
using BattleEvent;

public class PlayerPhysicsComponent : PhysicsComponent {

    private Player player;

    private ReceiverPlayer receiverSpawn;


    public void OnInit(Player player) {
        this.player = player;

        receiverSpawn = new ReceiverPlayer();
        receiverSpawn.RegisterDelegates();

    }

    public void OnUpdate(float delta) {

    }

}

public class ReceiverPlayer {
    public void RegisterDelegates() {
        EventManager.Instance.RegisterEvent(BattleEvent.EventType.spawnMagic, OnEventProcessSpawnMagic);
        EventManager.Instance.RegisterEvent(BattleEvent.EventType.hitAttack, OnEventProcessHitAttack);
    }

    public void UnRegisterDelegates() {
        EventManager.Instance.UnRegisterEvent(BattleEvent.EventType.spawnMagic);
        EventManager.Instance.UnRegisterEvent(BattleEvent.EventType.hitAttack);
    }

    private void OnEventProcessSpawnMagic(BaseEventMsg msg) {
        Debug.Log("onEventProcessSpawnMagic");
        if (msg != null && msg.paramObjects.Length > 0) {
            var player = msg.paramObjects[0] as Player;
            var magicBall = GameObject.Instantiate(player.magicBall, player.shootPoint.position, player.shootPoint.rotation);
            magicBall.GetComponent<MagicController>().velocity = magicBall.transform.forward * player.shootSpeed;
            GameManager.Instance.AddSpawn(magicBall.GetComponent<MagicController>(), player.gameObject.GetComponent<PlayerEntity>());
            //player.gameObject.GetComponent<PlayerEntity>().CmdSpawnGameObject(magicBall);
        }
    }

    private void OnEventProcessHitAttack(BaseEventMsg msg) {
        Debug.Log("onEventProcess hit attack");
        if (msg != null && msg.paramObjects.Length > 0) {
            uint id = (uint)msg.paramObjects[0];
            int damage = (int)msg.paramObjects[1];
            Player player = GameManager.Instance.GetFromId(id);
            player.gameObject.GetComponent<PlayerEntity>().health -= damage;
            player.graphicComponent.GetStab();
        }
    }
}

