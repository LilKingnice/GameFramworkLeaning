using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private string monsterName = "[巨魔]";
    public string MonsterName { get { return monsterName; } }
    [SerializeField]Player player1;
    [SerializeField]Player player2;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            whokilled(player1);
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            whokilled(player2);
        }
    }

    void whokilled(Player player)
    {
        Debug.Log("怪物死亡了！");
        EventCenter.Instance.EventTrigger<Monster, Player>(E_EventType.E_MonsterDie, this, player);
        Destroy(gameObject);
    }
    void OnDestroy()
    {
        EventCenter.Instance.ClearListener();
    }
}
