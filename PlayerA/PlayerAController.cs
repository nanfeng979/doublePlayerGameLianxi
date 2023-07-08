using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerSet.Instance.Player == 'A') {
            if(Input.GetKeyDown(KeyCode.A)) {
                transform.position += new Vector3(-1, 0, 0);
            } else if(Input.GetKeyDown(KeyCode.D)) {
                transform.position += new Vector3(1, 0, 0);
            }
        }
    }
}
