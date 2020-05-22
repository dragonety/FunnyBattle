using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace Reconstitution {
    public class UnetManager : NetworkManager {

        [Header("=====DIY=====")]

        public static UnetManager instance;

        public bool isServer;
        public bool isClient;

        private Dictionary<uint, uint> dictSpawnPlayer = new Dictionary<uint, uint>();

        private GameObject localPlayer;

        private void Awake() {
            instance = this;
        }

        public override void OnStartHost() {
            base.OnStartHost();
            Debug.Log("Start Host");
        }

        public override void OnStartServer() {
            base.OnStartServer();

            isServer = true;

            Debug.Log("Start Server");
        }

        public override void OnStartClient(NetworkClient client) {
            base.OnStartClient(client);

            isClient = true;

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

        public override void OnServerConnect(NetworkConnection conn) {
            base.OnServerConnect(conn);
            Debug.Log("Server Connect");
        }

        public override void OnClientConnect(NetworkConnection conn) {
            base.OnClientConnect(conn);
            Debug.Log("Client Connect");
        }

        public override void OnClientSceneChanged(NetworkConnection conn) {
            base.OnClientSceneChanged(conn);
            GameManager.Instance.Add<GameBattleBehaviour>();
            Debug.Log("Client scene change");
        }        

        public override void OnServerSceneChanged(string sceneName) {
            base.OnServerSceneChanged(sceneName);
            //GameManager.Instance.Add<GameBattleBehaviour>();
            Debug.Log("Server scene change");
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId) {
            base.OnServerAddPlayer(conn, playerControllerId);
            Debug.Log("Server add " + conn + playerControllerId);
        }

        public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader) {
            base.OnServerAddPlayer(conn, playerControllerId, extraMessageReader);
            Debug.Log("Server add" + conn + playerControllerId + extraMessageReader);
        }

        public void AddSpawn(uint spawn, uint player) {
            dictSpawnPlayer[spawn] = player;
        }

        public void RemoveSpawn(uint spawn) {
            if (dictSpawnPlayer.ContainsKey(spawn)) {
                dictSpawnPlayer.Remove(spawn);
            } else {
                Debug.Log("remove null spawn");
            }
        }

        public uint GetPlayer(uint spawn) {
            uint player;
            if (dictSpawnPlayer.TryGetValue(spawn, out player)) {
                return player;
            } else {
                Debug.Log("get null from dictSpawnPlayer");
                return spawn;
            }
        }

    }
}


