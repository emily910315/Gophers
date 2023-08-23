using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager m_instance = null;
    public static GameManager Instance
    {
        get
        {
            //無找到GameManager，則創建一個GameManager
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    [SerializeField] Text m_HpText = null; //生命值文字
    [SerializeField] Text m_ScoreText = null; //分數文字
    [SerializeField] ManagerUnit[] m_AllUnit = null; //所有的按紐

    private static int MAX_HP = 5;//生命值預設最大為5
    private int _m_Hp = MAX_HP;
    private int m_Hp
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

    private int _m_Score = 0;//分數預設為0
    private int m_Score
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

    private bool m_PlayerLive;//玩家存活    


    void Start()
    {
        m_PlayerLive = true;
        // 倒計時
        StartCountdown();
    }

    void Update()
    {
        for (int i = 0; i < m_AllUnit.Length; i++)
        {
            ManagerUnit unit = m_AllUnit[i];
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
    private void StartCountdown()
    {
        //開始倒數計時的協程
        StartCoroutine(Countdown());
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

    public void AddScore(int pointsToAdd)
    {
        if (!m_PlayerLive)
            return;

        m_Score += pointsToAdd;
        m_ScoreText.text = m_Score.ToString(); // 更新分數文字顯示
    }
}
