using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

public class PlayerNetMoveComponent : MoveComponent {

    private Player player;

    public void OnInit(Player player) {
        this.player = player;
    }

    public void OnUpdate(float delta) {
        player.transform.position = Vector3.Lerp(player.transform.position, player.position, Time.deltaTime * player.lerpRate);
        player.transform.rotation = Quaternion.Lerp(player.transform.rotation, player.rotation, Time.deltaTime * player.lerpRate);
    }

}