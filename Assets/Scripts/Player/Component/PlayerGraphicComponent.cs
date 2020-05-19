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

        if (Input.GetAxis("Vertical") > 0 || Input.GetAxis("Horizontal") > 0) {
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }

        if (Input.GetMouseButtonDown(0) && !animator.GetBool("isAttack")) {
            animator.SetBool("isAttack", true);
        } 

        if (Input.GetMouseButtonDown(1)) {
            Debug.Log("block");
            animator.SetBool("isBlock", true);
        } else {
            animator.SetBool("isBlock", false);
        }

        if (Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("spawn" + animator.GetBool("isAttack"));
            animator.SetBool("isSpawn", true);
            Debug.Log("after spawn" + animator.GetBool("isAttack"));
        } else {
            animator.SetBool("isSpawn", false);
        }

        if (player.health <= 0) {
            animator.SetBool("isDeath", true);
        } else {
            animator.SetBool("isDeath", false);
        }
    }
}