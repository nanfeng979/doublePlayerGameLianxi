using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllObjController : MonoBehaviour
{
    public static AllObjController Instance;

    void Start()
    {
        if(Instance == null) {
            Instance = this;
        }
    }

    // 对用户的数据进行解析和控制
    public void Controller(PlayerData playerData) {
        Move(playerData.playerName, playerData.position);
    }

    // 移动
    private void Move(string playerName, Vector3 positionOffset) {
        GameObject.Find(playerName).transform.position = positionOffset;
    }

}
