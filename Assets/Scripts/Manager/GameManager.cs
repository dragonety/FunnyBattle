using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameManager : NetworkManager {

    static GameManager instance;
    public static GameManager Instance {
        get {
            return instance ?? (instance = new GameManager());
        }
    }

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
		if(dictSpawnPlayer.TryGetValue(spawn, out player)) {
            return player;
		} else {
            Debug.Log("get null from dictSpawnPlayer");
            return spawn;
		}
	}

    public override void OnStartHost() {
        base.OnStartHost();
        Debug.Log("Start Host");
    }

    public override void OnStartServer() {
        base.OnStartServer();
        Debug.Log("Start Server");
    }

    public override void OnStartClient(NetworkClient client) {
        base.OnStartClient(client);
        Debug.Log("Start Client");
    }

    public override void OnStopHost() {
        base.OnStopHost();
        Debug.Log("Stop Host");
    }

    public override void OnStopServer() {
        base.OnStopServer();
        Debug.Log("Stop Server");
    }

    public override void OnStopClient() {
        base.OnStopClient();
        Debug.Log("Stop Client");
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
        base.OnServerAddPlayer(conn, playerControllerId);
        Debug.Log("Server add " + conn + playerControllerId);
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader) {
        base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
        Debug.Log("Server add" + conn + playerControllerId + extraMessageReader);
    }

}
