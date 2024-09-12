using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private string myName = "玩家一";

    public string PlayerName { get { return myName; } set { myName = value; } }
    // Start is called before the first frame update
    void Start()
    {
        EventCenter.Instance.AddListener<Monster, Player>(E_EventType.E_MonsterDie, PlayerWin);
    }
    void PlayerWin(Monster info, Player info2)
    {
        if (info2.PlayerName == myName)
        {
            Debug.Log($"怪兽{info.MonsterName}死亡，由{info2.PlayerName}击败！！！");

            gameObject.transform.localScale = new Vector3(1, 2, 1);
        }
    }
    /// <summary>
    /// Called when the current GameObject is destroyed
    /// </summary>
    void OnDestroy()
    {
        EventCenter.Instance.RemoveListener<Monster, Player>(E_EventType.E_MonsterDie, PlayerWin);
        Debug.Log("Player销毁,移除事件监听");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
