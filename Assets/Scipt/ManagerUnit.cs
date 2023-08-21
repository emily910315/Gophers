using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManagerUnit : MonoBehaviour
{

    private float m_SwitchTime;//隨機時間
    private Text m_HitTimes = null;//點擊次數

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

        GameManager.Instance.ResetHp();//分數增加
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
    }
}
