using UnityEngine;
using UnityEditor;

public class PlayerPhysicsComponent : PhysicsComponent {

    private Player player;

    public void OnInit(Player player) {
        this.player = player;
    }

    public void OnUpdate(float delta) {
        
    }
}