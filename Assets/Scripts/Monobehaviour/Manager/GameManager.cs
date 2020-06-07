using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {

    private Dictionary<NetworkInstanceId, NetworkInstanceId> dictSpawnPlayer = new Dictionary<NetworkInstanceId, NetworkInstanceId>();
    private Dictionary<uint, Player> dictPlayer = new Dictionary<uint, Player>();

    public GameObject localPlayer;

    public void AddPlayer(uint id, Player player) {
        if (dictPlayer.ContainsKey(id)) {
            Debug.LogError("already have the player");
        } else {
            dictPlayer.Add(id, player);
        }
    }

    public void RemovePlayer(uint id, Player player) {
        if (dictPlayer.ContainsKey(id)) {
            dictPlayer.Remove(id);
        } else {
            Debug.LogError("don't have the player");
        }
    }

    public Player GetFromId(uint id) {
        if (dictPlayer.ContainsKey(id)) {
            return dictPlayer[id];
        } else {
            return null;
        }
    }

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

    public void GameOver() {
        StopClient();
        StopHost();
    }

    public void StartHost() {
        NetworkManager.singleton.StartHost();
    }

    public void StartClient() {
        NetworkManager.singleton.StartClient();
    }

    public void StopClient() {
        NetworkManager.singleton.StopClient();
    }

    public void StopHost() {
        NetworkManager.singleton.StopHost();
    }
}