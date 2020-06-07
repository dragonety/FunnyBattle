using UnityEngine;

class PlayerMoveComponet : MoveComponent {

    private Player player;

    public bool canMove = true;

    public void OnInit(Player player) {
        this.player = (Player)player;
        canMove = true;
    }

    public void OnUpdate(float delta) {
        if (canMove) {
            Move(GetInputData());
        }
    }

    private Vector3 GetInputData() {
        var hor = Input.GetAxis("Horizontal");
        var ver = Input.GetAxis("Vertical");

        var move = hor * player.transform.right + ver * player.transform.forward;

        if (move.magnitude > 1) {
            move.Normalize();
        }
        move = player.transform.InverseTransformDirection(move);
        return move;
    }

    private void Move(Vector3 move) {
        if (move.magnitude > 0) {
            player.transform.Rotate(0.0f, move.x * player.rotateSpeed * Time.deltaTime, 0.0f);
            player.transform.Translate(Vector3.forward * move.z * player.moveSpeed * Time.deltaTime);
        } 
    }
}