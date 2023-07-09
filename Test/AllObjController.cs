using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AllObjController : MonoBehaviour
{
    public static AllObjController Instance;

    public Text text;

    void Start()
    {
        Instance = this;
    }

    // 对用户的数据进行解析和控制
    public void Controller(PlayerData playerData) {
        Move(playerData.playerName, playerData.position);
        SetText(playerData.position.ToString());
    }

    // 移动
    private void Move(string playerName, Vector3 positionOffset) {
        GameObject.Find(playerName).transform.position = positionOffset;
    }

    private void SetText(string content) {
        text.text = content;
    }


}
