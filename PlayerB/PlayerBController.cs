using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBController : MonoBehaviour
{
    private PlayerData myData = new PlayerData();
    void Start()
    {
        myData.playerName = gameObject.name;
        myData.position = transform.position;
        myData.spriteColor = GetComponent<SpriteRenderer>().color;
        OperationSendMessage.Instance.SendMessage_(JsonUtility.ToJson(myData).ToString());
    }

    void Update()
    {
        if(gameObject.name != "PlayerB") {
            return;
        }

        // 监听左右移动输入
        float vertical = Input.GetAxis("Vertical");
        if(vertical != 0) {
            myData.position = transform.position + new Vector3(0, vertical, 0) * Time.deltaTime;
            // 发送新数据
            OperationSendMessage.Instance.SendMessage_(JsonUtility.ToJson(myData).ToString());
        }
    }
}
