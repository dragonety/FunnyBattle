using UnityEngine;
using UnityEditor;

public class PlayerBlockState : PlayerBaseState {

    public PlayerBlockState(AnimatePlayer player) {
        this.player = player;
    }

    public override void HandleInput() {
        throw new System.NotImplementedException();
    }
}