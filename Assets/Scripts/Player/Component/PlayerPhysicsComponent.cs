using UnityEngine;
using UnityEditor;
using BattleEvent;

public class PlayerPhysicsComponent : PhysicsComponent {

    private Player player;

    public void OnInit(Player player) {
        this.player = player;
    }

    public void OnUpdate(float delta) {
        
    }


}

public class ReceiverSpawn {
	public void RegisterDelegates() {
        EventManager.Instance.RegisterEvent(BattleEvent.EventType.spawnMagic, OnEventProcessSpawnMagic);
	}

	private void OnEventProcessSpawnMagic(BaseEventMsg msg) {
        Debug.Log("receive process spawn magic");
	}
}