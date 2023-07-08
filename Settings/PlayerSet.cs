using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSet : MonoBehaviour
{
    public char Player;
    public static PlayerSet Instance;
    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        if(this != null) {
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerA() {
        Player = 'A';
        nextScene();
    }

    public void SetPlayerB() {
        Player = 'B';
        nextScene();
    }

    public void nextScene() {
        SceneManager.LoadScene("Play");
    }
}
