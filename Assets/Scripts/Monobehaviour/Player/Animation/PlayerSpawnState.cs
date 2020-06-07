using UnityEngine;
using UnityEditor;

public class PlayerSpawnState : PlayerBaseState {

    public PlayerSpawnState(AnimatePlayer player) {
        this.player = player;
    }

    public override void HandleInput() {
        throw new System.NotImplementedException();
    }
}