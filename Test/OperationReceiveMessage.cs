using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OperationReceiveMessage : MonoBehaviour
{
    public static OperationReceiveMessage Instance;

    public GameObject prefabA;
    public GameObject prefabB;
    // 维护一个所有对象的列表
    public List<string> objList = new List<string>();

    public PlayerData playerData;

    void Awake()
    {
        if(Instance == null) {
            Instance = this;
        }
    }

    void Update() {
        // 监控数据是否变化
        if(playerData != null) {
            // 有数据时对该数据进行处理，然后销毁数据
            Controller(playerData);
            playerData = null;
        }    
    }

    // 对用户的数据进行解析和控制
    public void Controller(PlayerData playerData) {
        if(!objList.Contains(playerData.playerName)) {
            CreateObj(playerData.playerName);
            objList.Add(playerData.playerName);
            return;
        }
        Move(playerData.playerName, playerData.position);
    }

    // 创建对象
    private void CreateObj(string playerName) {
        GameObject obj = new GameObject();
        if(playerName == "PlayerA") {
            obj = Instantiate(prefabA, new Vector3(0, 0, 0), Quaternion.identity);
            if(PlayerSet.Instance.playerName != "PlayerA") {
                obj.GetComponent<PlayerAController>().enabled = false;
            }
        } else if(playerName == "PlayerB") {
            obj = Instantiate(prefabB, new Vector3(0, 0, 0), Quaternion.identity);
            if(PlayerSet.Instance.playerName != "PlayerB") {
                obj.GetComponent<PlayerBController>().enabled = false;
            }
        }
        obj.name = playerName;
        obj.GetComponent<SpriteRenderer>().color = UnityEngine.Random.ColorHSV();
    }

    // 移动
    private void Move(string playerName, Vector3 position) {
        GameObject.Find(playerName).transform.position = position;
    }

}
