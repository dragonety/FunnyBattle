using BattleEvent;
using UnityEngine;

public class PlayerNetGraphicComponent : GraphicComponent {

    private Player player;
    private Animator animator;

    public void OnInit(Player player) {
        this.player = player;
        animator = player.gameObject.GetComponent<Animator>();
    }

    public void OnUpdate(float delta) {

    }

    public void GetHurt() {
        animator.Play("GetHurt");
    }

    public void GetStab() {
        animator.Play("GetStab");
    }


}