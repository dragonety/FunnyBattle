using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using Reconstitution;

public class UIManager : SingletonMonobehaviour<UIManager> {

    enum UIEnum {
        login,
        lobby,
        game
    }

    [SerializeField]
    private GameObject[] listUI;
    
    private UIEnum curUI;

    private void Start() {
        Debug.Log("start");
        curUI = UIEnum.login;
        DontDestroyOnLoad(gameObject);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            SwitchTo(curUI);
        }
    }

    public void StartHost() {
        //GameManager.Instance.StartHost();
        UnetManager.instance.StartHost();
        SwitchTo(UIEnum.game);
        SwitchTo(UIEnum.game);
    }

    public void StartClient() {
        //GameManager.Instance.StartClient();
        UnetManager.instance.StartClient();
        SwitchTo(UIEnum.game);
        SwitchTo(UIEnum.game);
    }

    public void StopHost() {
        //GameManager.Instance.StopHost();
        UnetManager.instance.StopHost();
        SwitchTo(UIEnum.lobby);
    }

    public void StopClient() {
        //GameManager.Instance.StopClient();
        UnetManager.instance.StopClient();
        SwitchTo(UIEnum.lobby);
    }

    public void Login() {
        SceneManager.LoadScene("Lobby");
        SwitchTo(UIEnum.lobby);
    }

    private void SwitchTo(UIEnum next) {
        if (curUI == next) {
            listUI[(int)curUI].SetActive(!listUI[(int)curUI].activeSelf);
        } else {
            listUI[(int)curUI].SetActive(false);
            listUI[(int)next].SetActive(true);
            curUI = next;
        }
    }

}