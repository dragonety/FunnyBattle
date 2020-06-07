using UnityEngine;
using UnityEditor;

public class PlayerWalkState : PlayerBaseState {

    public PlayerWalkState(AnimatePlayer player) {
        this.player = player;
    }

    public override void HandleInput() {
        
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("idel to Attack");
            player.SetPlayerState(new PlayerAttackState(player));
        }
        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("idel to Block");
            player.SetPlayerState(new PlayerBlockState(player));
        }
        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("idel to Spawn");
            player.SetPlayerState(new PlayerSpawnState(player));
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0) {
            Debug.Log("walk to idle");
            player.SetPlayerState(new PlayerIdleState(player));
        }
    }
}