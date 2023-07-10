using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAController : MonoBehaviour
{
    private PlayerData myData = new PlayerData();
    void Start()
    {
        myData.playerName = gameObject.name;
    }

    void Update()
    {
        if(gameObject.name != "PlayerA") {
            return;
        }
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(moveHorizontal != 0) {
            myData.position = transform.position + new Vector3(moveHorizontal, 0, 0) * Time.deltaTime;
            // 发送新数据
            OperationSendMessage.Instance.SendMessage_(JsonUtility.ToJson(myData).ToString());
        }
    }
}
