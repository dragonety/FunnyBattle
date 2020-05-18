using UnityEngine;
using UnityEditor;
using UnityEngine.Networking;

public class UIManager : SingletonMonobehaviour<UIManager> {

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            Trigger();
        }
    }

    public void StartHost() {
        NetworkManager.singleton.StartHost();
        Trigger();
    }

    public void StartClient() {
        NetworkManager.singleton.StartClient();
        Trigger();
    }

    private void Trigger() {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}