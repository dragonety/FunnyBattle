using BattleEvent;
using UnityEngine;

public class PlayerGraphicComponent : GraphicComponent {

    private Player player;
    private Animator animator;

    public void OnInit(Player player) {
        this.player = player;
        animator = player.gameObject.GetComponent<Animator>();
    }

    public void OnUpdate(float delta) {

        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if (Input.GetAxis("Vertical") != 0 || Input.GetAxis("Horizontal") != 0) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetMouseButtonDown(0) && !animator.GetBool("isAttack")) {
            animator.SetBool("isAttack", true);
            EventManager.Instance.SendEvent(BattleEvent.EventType.attack, Time.time);
        } 

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("block");
            animator.SetBool("isBlock", true);
        } else {
            animator.SetBool("isBlock", false);
        }

        if (Input.GetKeyDown(KeyCode.E) && !animator.GetBool("isSpawn")) {
            animator.SetBool("isSpawn", true);
            EventManager.Instance.SendEvent(BattleEvent.EventType.spawnMagic, player);
        }

        if (player.health <= 0) {
            animator.SetBool("isDeath", true);
        } else {
            animator.SetBool("isDeath", false);
        }
    }

    public void GetHurt() {
        animator.Play("GetHurt");
    }

    public void GetStab() {
        animator.Play("GetStab");
    }

    
}