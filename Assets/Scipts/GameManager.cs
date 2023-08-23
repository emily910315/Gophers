using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static GameManager m_instance = null;
    public static GameManager Instance
    {
        get
        {
            //無找到則創建一個GameManager
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    [SerializeField] Text m_HpText = null; //生命值文字
    [SerializeField] Text m_ScoreText = null; //分數文字

    [SerializeField] GameObject UnitsGrid = null; //所有按鈕的父物件
    [SerializeField] ManagerUnit[] m_AllUnits = null; //所有的按紐

    static int MAX_HP = 5;
    int _m_Hp = MAX_HP;
    int m_Hp
    {
        get
        {
            return _m_Hp;
        }
        set
        {
            _m_Hp = value;
            m_HpText.text = _m_Hp.ToString();//顯示更新生命值
        }
    }

    int _m_Score = 0;//分數預設為0
    int m_Score
    {
        get
        {
            return _m_Score;
        }
        set
        {
            _m_Score = value;
            m_ScoreText.text = _m_Score.ToString();//顯示更新分數
        }
    }

    bool m_PlayerLive;//玩家存活    

    //float lastClickTime = 0f; // 上次點擊的時間
    //float doubleClickThreshold = 0.3f; //雙擊值域
    //bool isDoubleClick = false; // 是否是雙擊

    void Start()
    {
        m_AllUnits = UnitsGrid.GetComponentsInChildren<ManagerUnit>();
        m_PlayerLive = true;
        // 倒計時
        StartCoroutine(Countdown());
    }

    void Update()
    {
        foreach (ManagerUnit unit in m_AllUnits)
        {
            if (unit.CheckTime())
            {
                //檢查按鈕重新生成時間，若到達則重新生成
                unit.SwitchActive();
            }
        }

        if (!m_PlayerLive)
            return;
    }

    public void ResetHp()
    {
        if (!m_PlayerLive)
            return;

        m_Hp = MAX_HP;
    }

    public void UpdateScore(int score)
    {
        if (!m_PlayerLive)
            return;

        m_Score += score;
    }

    private IEnumerator Countdown()
    {
        //協程
        for (; m_Hp > 0; m_Hp--)
        {
            yield return new WaitForSeconds(1f); // 等待1秒
        }

        m_PlayerLive = false;
    }
}
