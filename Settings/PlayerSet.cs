using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSet : MonoBehaviour
{
    public string playerName;
    public static PlayerSet Instance;
    void Start()
    {
        Instance = this;

        // DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerA() {
        playerName = "PlayerA";
        nextScene();
    }

    public void SetPlayerB() {
        playerName = "PlayerB";
        nextScene();
    }

    public void nextScene() {
        SceneManager.LoadScene("test");
    }
}
