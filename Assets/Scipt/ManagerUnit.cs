using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManagerUnit : MonoBehaviour
{
    
    private float m_Time;//隨機時間

    //private bool m_IsLive = false;//按鈕是否存活
    private bool m_IsCanClick = false;//點擊按鈕

    private float m_Hp=0;//生命值
    private int m_MaxHp;//最大生命值
    //private Dictionary<ManagerUnit, int> m_MaxHpDict = new Dictionary<ManagerUnit, int>();

    private Text m_HitTimes = null;//點擊次數

    
    void Start()
    {
        Disable();
        m_Hp = m_MaxHp; // 設定按鈕的當前生命值為最大生命值
    }
    private void Awake()
    {
        //點擊次數顯示
        m_HitTimes = gameObject.GetComponentInChildren<Text>();
    }


    void Update()
    {

    }


    public void OnClickMonster()
    {   
        //按鈕點擊事件
        gameObject.SetActive(false);

        if (m_IsCanClick == false)
        {
             m_Hp--;//如果不點擊則生命值-1
        }
        //m_HitTimes.text = m_Hp.ToString();//更新點擊次數

        
        if (m_Hp <= 0)//檢查生命值是否<0
        {
            gameObject.SetActive(false); ;//按鈕關閉
        }
    }

    public bool CheckTime() => Time.time > m_Time;//檢查當下時間是否大於隨機產生時間
 
    public void Disable()
    {
        gameObject.SetActive(false);//清空所有按鈕
        AddTime();
    }

    private void AddTime()
    {
        //隨機時間內產生按鈕
        m_Time = Time.time + Random.Range(1f, 3f);
    }

    public void SwitchActive()
    {   
        gameObject.SetActive(!gameObject.activeSelf);//觸發事件後按鈕產生回應
        AddTime();
    }
}
