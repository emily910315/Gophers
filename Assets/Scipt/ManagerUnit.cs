using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManagerUnit : MonoBehaviour
{
    private float m_Time = 0;
    //private bool m_IsLive = false;//按鈕是否存活
    //private bool m_IsCanClick = false;//是否點擊按鈕

    //private float m_Hp=0;//生命值
    //private int m_MaxHp;//最大生命值
    //private Dictionary<ManagerUnit, int> m_MaxHpDict = new Dictionary<ManagerUnit, int>();

    //private Text m_HitTimes = null;

    void Start()
    {
        Disable();
        //m_Hp = m_MaxHp; // 設定按鈕的當前生命值為最大生命值
    }

    void Update()
    {
        //if (m_IsLive)
        //{
        //    if (transform.localScale.x < 1)
        //    {//按紐大小生成中
        //        transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime;
        //    }
        //    else
        //    {//按鈕生成至一定大小才可點擊
        //        m_IsCanClick = true;

        //    }

        //    if (m_IsCanClick)
        //    {
        //        m_RemoveTime += Time.deltaTime;//按鈕已存在時間
        //        /*int limitTime = 5 - GameManager.Instance.GetHardRank();
        //        if (m_RemoveTime > limitTime)
        //        {//按鈕未在已存在時間內點擊
        //            GameManager.Instance.Hit();
        //            OnDie();//按鈕消失
        //        }*/
        //    }
        //}
    }

    public void OnClickMonster()
    {
        SwitchActive();
        //按鈕點擊事件
        //if (m_IsCanClick == true)
        //{
        //    return;
        //}
        //m_Hp--;//如果不點擊則生命值-1

        //m_HitTimes.text = m_Hp.ToString();//更新點擊次數
        //if (m_Hp <= 0)
        //{//檢查生命值是否<0
        //    //int score = (GameManager.Instance.GetHardRank() == 1) ? 1 : 2;
        //    GameManager.Instance.AddScore(1);
        //    OnDie();//按鈕消失
        //}
        //m_IsLive = false;//無按鈕
    }

    private void OnDie()
    {//按鈕消失
        StartCoroutine(FadeOut());//漸隱效果
        gameObject.SetActive(false);//無按鈕
        //m_IsLive = false;//按鈕不出現
    }

    private IEnumerator FadeOut()
    {
        float duration = 1.0f;//按鈕出現到消失的時間
        float elapsedTime = 0.01f;//按鈕剛出現時間

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration); // 按鈕漸隱效果
            Color newColor = new Color(1f, 1f, 1f, alpha); // 得到按鈕完整顏色效果
            gameObject.GetComponent<Renderer>().material.color = newColor;

            elapsedTime += Time.deltaTime; // 更新已過去的時間
            yield return null; // 等待下一幀
        }

        gameObject.SetActive(true);
        //m_IsLive = true; // 有按鈕
    }
    public bool CheckTime()
    {

        return Time.time > m_Time;//檢查重新出現時間是否已大於當前時間
    }

    private void AddTime()
    {
        m_Time = Time.time + Random.Range(1f, 3f);
    }

    public void SwitchActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);
        AddTime();
    }

    private void Disable()
    {
        gameObject.SetActive(false);
        AddTime();
    }
}
