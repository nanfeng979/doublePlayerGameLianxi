using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAController : MonoBehaviour
{
    private PlayerData myData = new PlayerData();
    void Start()
    {
        myData.playerName = gameObject.name;
        // myData.position = transform.position;
        // myData.spriteColor = GetComponent<SpriteRenderer>().color;
        // OperationSendMessage.Instance.SendMessage_(JsonUtility.ToJson(myData).ToString());
    }

    void Update()
    {
        if(gameObject.name != "PlayerA") {
            return;
        }

        // 监听左右移动输入
        float moveHorizontal = Input.GetAxis("Horizontal");
        if(moveHorizontal != 0) {
            myData.position = transform.position + new Vector3(moveHorizontal, 0, 0) * Time.deltaTime;
            myData.spriteColor = GetComponent<SpriteRenderer>().color;
            // 发送新数据
            OperationSendMessage.Instance.SendMessage_(JsonUtility.ToJson(myData).ToString());
        }
    }
}
