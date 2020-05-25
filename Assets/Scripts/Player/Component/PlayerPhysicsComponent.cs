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
        CheckDeath(player.health);
    }

    private void CheckDeath(int health) {
        if (health <= 0) {
            NetManager.singleton.StopClient();
		}
	}

}

public class ReceiverPlayer {

    private bool debug = true;

    public void RegisterDelegates() {
        EventManager.Instance.RegisterEvent(BattleEvent.EventType.spawnMagic, OnEventProcessSpawnMagic);
        EventManager.Instance.RegisterEvent(BattleEvent.EventType.hitAttack, OnEventProcessHitAttack);
    }

    public void UnRegisterDelegates() {
        EventManager.Instance.UnRegisterEvent(BattleEvent.EventType.spawnMagic);
        EventManager.Instance.UnRegisterEvent(BattleEvent.EventType.hitAttack);
    }

    private void OnEventProcessSpawnMagic(BaseEventMsg msg) {
        if (debug) Debug.Log("onEventProcessSpawnMagic");
        if (msg != null && msg.paramObjects.Length > 0) {
            var player = msg.paramObjects[0] as Player;
            var magicBall = GameObject.Instantiate(player.magicBall, player.shootPoint.position, player.shootPoint.rotation);
            //magicBall.GetComponent<MagicController>().velocity = magicBall.transform.forward * player.shootSpeed;
            //GameManager.Instance.AddSpawn(magicBall.GetComponent<MagicController>(), player.gameObject.GetComponent<PlayerEntity>());
            //player.gameObject.GetComponent<PlayerEntity>().CmdSpawnGameObject(magicBall);
        }
    }

    private void OnEventProcessHitAttack(BaseEventMsg msg) {
        if (debug) Debug.Log("onEventProcess hit attack");
        if (msg != null && msg.paramObjects.Length > 0) {
            uint id = (uint)msg.paramObjects[0];
            int damage = (int)msg.paramObjects[1];
            Vector3 hitPoint = (Vector3)msg.paramObjects[2];
            Player player = GameManager.Instance.GetFromId(id);
            player.gameObject.GetComponent<PlayerEntity>().health -= damage;
            player.graphicComponent.GetStab();
            EffectManager.Instance.ShowEffect("Player", hitPoint, Quaternion.identity);
        }
    }
}

