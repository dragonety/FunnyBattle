using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

    private Dictionary<NetworkInstanceId, NetworkInstanceId> dictSpawnPlayer = new Dictionary<NetworkInstanceId, NetworkInstanceId>();

    private GameObject localPlayer;

    public void AddSpawn(NetworkInstanceId spawn, NetworkInstanceId player) {
        dictSpawnPlayer[spawn] = player;
    }

    public void AddSpawn(NetworkBehaviour spawnBehav, NetworkBehaviour playerBehav) {
        dictSpawnPlayer[spawnBehav.netId] = playerBehav.netId;
    }
    public void RemoveSpawn(NetworkInstanceId spawn) {
        if (dictSpawnPlayer.ContainsKey(spawn)) {
            dictSpawnPlayer.Remove(spawn);
        } else {
            Debug.Log("remove null spawn");
        }
    }

    public NetworkInstanceId GetPlayer(NetworkInstanceId spawn) {
        NetworkInstanceId player;
        if (dictSpawnPlayer.TryGetValue(spawn, out player)) {
            return player;
        } else {
            Debug.Log("get null from dictSpawnPlayer");
            return spawn;
        }
    }
}