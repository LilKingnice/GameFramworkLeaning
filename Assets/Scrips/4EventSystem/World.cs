using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class World : MonoBehaviour
{
    RectTransform rect;
    Vector2 rectStartPos;
    //Rect testrect;
    private Text text;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        rect = GetComponent<RectTransform>();

        rectStartPos = text.rectTransform.anchoredPosition;
        Debug.LogWarning(text.preferredWidth);
        EventCenter.Instance.AddListener<Monster, Player>(E_EventType.E_MonsterDie, Bulletin);
        //EventCenter.Instance.AddListener<Player>(E_EventType.E_MonsterDie,Bulletin2);
    }
    void Bulletin(Monster info, Player info2)
    {
        // text.text = $"怪兽{info.MonsterName}死亡";
        // Debug.Log($"怪兽{info.MonsterName}死亡全频道通知！！！");
        text.text = $"怪兽{info.MonsterName}死亡!由{info2.PlayerName}杀死！";
        Debug.Log($"怪兽{info.MonsterName}死亡,由{info2.PlayerName}杀死！全频道通知！！！");
        StartCoroutine(move());
    }
    void OnDestroy()
    {
        EventCenter.Instance.RemoveListener<Monster, Player>(E_EventType.E_MonsterDie, Bulletin);
        //EventCenter.Instance.RemoveListener<Player>(E_EventType.E_MonsterDie, Bulletin2);
        Debug.Log("World销毁,移除事件监听");
        StopCoroutine(move());
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator move()
    {
        Vector2 pos;
        while (true)
        {
            pos = rect.anchoredPosition;
            pos.x -= Time.deltaTime * speed;
            rect.anchoredPosition = pos;
            yield return null;
            // if (text.rectTransform.anchoredPosition.x <= -rectStartPos.x)
            // {
            //     yield return new WaitForSeconds(2f);
            // }
            if (text.rectTransform.anchoredPosition.x <= -rectStartPos.x - text.preferredWidth)
            {

                Debug.Log("Text has finished scrolling.");
                // 这里可以做文本滚动完成后的处理，比如重置位置开始新一轮滚动，或者停止滚动等
                break;
            }
        }
    }
}
