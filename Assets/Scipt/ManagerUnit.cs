using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManagerUnit : MonoBehaviour
{

    private float m_SwitchTime;//隨機時間
    private Text m_HitTimes = null;//點擊次數
    private int m_ScoreValue = 0; // 預設為 0 分
    public Color redColor; // 紅色
    public Color blueColor; // 藍色


    private void Awake()
    {
        //點擊次數顯示
        m_HitTimes = gameObject.GetComponentInChildren<Text>();
    }

    void Start()
    {
        Disable();
        
    }

    public void OnClickMonster()
    {
        //按鈕點擊事件
        SwitchActive();

        GameManager.Instance.ResetHp();//重製生命值
        //GameManager.Instance.AddScore(1); // 增加一分

        Image buttonImage = GetComponent<Image>();
        if (GetComponent<Image>().color == redColor)
        {
            GameManager.Instance.AddScore(1); // 加一分
        }
        else if (GetComponent<Image>().color  == blueColor)
        {
            GameManager.Instance.SubtractScore(1); // 扣一分
        }


    }

    public bool CheckTime() => Time.time > m_SwitchTime;//檢查當下時間是否大於隨機產生時間

    public void Disable()
    {
        gameObject.SetActive(false);//清空所有按鈕
        AddTime();
    }

    private void AddTime()
    {
        m_SwitchTime = Time.time + Random.Range(1f, 3f);
    }

    public void SwitchActive()
    {
        gameObject.SetActive(!gameObject.activeSelf);//觸發事件後按鈕產生回應
        AddTime();

        Image buttonImage = GetComponent<Image>();
        if (m_ScoreValue > 0)
        {
            buttonImage.color = redColor; // 設定紅色
        }
        else if (m_ScoreValue < 0)
        {
            buttonImage.color = blueColor; // 設定藍色
        }


    }


}
