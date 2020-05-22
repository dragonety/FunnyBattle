using UnityEngine;

namespace Reconstitution {
    public class AnimatorFeature : ObjectFeature {

        public Animator animator;

        public bool isMoving = false;

        public AnimatorFeature(GameObject gameObject) : base(gameObject) {
            animator = GetComponent<Animator>();
        }

        public void Attack() {
            animator.SetBool("isAttack", true);
        }

        public void Block() {
            animator.SetBool("isBlock", true);
        }

        public void Spawn() {
            animator.SetBool("isSpawn", true);
        }

        public void Move() {
            animator.SetBool("isMoving", true);
        }

        public void Stop() {
            animator.SetBool("isMoving", false);
        }

    }
}