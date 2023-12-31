using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class OperationReceiveMessage : MonoBehaviour
{
    public static OperationReceiveMessage Instance;

    public GameObject prefabA;
    public GameObject prefabB;
    // 维护一个所有对象的列表
    public List<string> objList = new List<string>();

    public PlayerData playerData = new PlayerData();
    private GameObject selfObj;

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }

    void Update() {
        // 监控数据是否变化
        if(playerData != null) {
            // 更新数据，然后销毁数据
            Controller(playerData);
            playerData = null;
        }
    }

    // 对用户的数据进行解析和控制
    public void Controller(PlayerData playerData) {
        // 实例化对象，每个对象仅执行以此
        if(!objList.Contains(playerData.playerName)) {
            CreateObj(playerData.playerName);
            objList.Add(playerData.playerName);
            return;
        }

        Move(playerData.playerName, playerData.position);
    }

    // 实例化对象，仅执行一次
    private void CreateObj(string playerName) {
        if(playerName == "PlayerA") {
            selfObj = Instantiate(prefabA, new Vector3(-1, 0, 0), Quaternion.identity);
            selfObj.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0, 1);
        } else if(playerName == "PlayerB") {
            selfObj = Instantiate(prefabB, new Vector3(1, 0, 0), Quaternion.identity);
            selfObj.GetComponent<SpriteRenderer>().color = new Color(1, 0, 1, 1);
        }
        selfObj.name = playerName;

        if(PlayerSet.Instance.playerName == playerName) {
            
        } else {
            selfObj.GetComponent<PlayerController>().enabled = false;
        }
    }

    // 实例化对象后发送初始化数据
    private void sendCreateInitData(ref GameObject obj) {
        PlayerData tempData = new PlayerData();
        tempData.playerName = obj.name;
        tempData.position = obj.transform.position;
        if(obj.GetComponent<SpriteRenderer>()) {
            tempData.spriteColor = obj.GetComponent<SpriteRenderer>().color;
        }
        
        OperationSendMessage.Instance.SendMessage_(JsonUtility.ToJson(tempData).ToString());
    }

    // 移动
    private void Move(string playerName, Vector3 position) {
        GameObject.Find(playerName).transform.position = position;
    }

    // 改变外观
    private void ChangeSkin(string playerName, Color color) {
        GameObject.Find(playerName).GetComponent<SpriteRenderer>().color = color;
    }

}
