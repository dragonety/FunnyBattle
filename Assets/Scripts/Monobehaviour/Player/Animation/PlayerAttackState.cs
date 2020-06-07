using UnityEngine;
using UnityEditor;

public class PlayerAttackState : PlayerBaseState {

    public PlayerAttackState(AnimatePlayer player) {
        this.player = player;
    }

    public override void HandleInput() {
        throw new System.NotImplementedException();
    }
}