using UnityEngine;
using UnityEditor;

public class AnimatePlayer{

    PlayerBaseState state;

    public AnimatePlayer() {
        state = new PlayerIdleState(this);
    }

    public void SetPlayerState(PlayerBaseState state) {
        this.state = state;
    }

    public void Update() {
        state.HandleInput();
    }

}