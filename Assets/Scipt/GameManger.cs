using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public static GameManger m_Main = null;
    public Text m_LiveValue = null;
    public Text m_Score = null;
    public MangerUnit[] m_AllUnit=null;
    public int m_NowScore = 0;
    private int m_Live = 5;
    private bool m_PlayerLive = true;
    private int m_HankRank = 1;

    private bool isCountingDown = false; // 是否正在倒數
    private float countdownTime = 5f; // 倒數的初始時間
    private float countdownTimer = 0f; // 倒數計時器
    private float lastClickTime = 0f; // 上次點擊的時間
    private float doubleClickThreshold;//雙擊值域
    private bool isDoubleClick = false; // 是否是雙擊
    private void Start()
    {
        // 倒計時
        StartCountdown();
    }

    private void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        m_Live = 5; // 生命初始值
        m_LiveValue.text = m_Live.ToString();
        yield return new WaitForSeconds(1f); // 等待1秒
        // 從5開始倒數到0
        for (int i = 5; i >= 0; i--)
        { 

            m_Live = i;
            m_LiveValue.text = m_Live.ToString();
            yield return new WaitForSeconds(1f); // 等待1秒
        }
        // 最後倒數到0
        m_Live = 0;
        m_LiveValue.text = m_Live.ToString();

        if (m_Live <= 0)
        {
            PlayerDie();
        }
    }
    private void Awake()
    {
        m_Main = this;
    }
    public int GetHardRank()
    {
        return m_HankRank;
    }
    public void Hit()
    {
        m_Live -= 1;//生命值扣1
        if (m_Live < 0)
        {
            m_Live = 0;
        }//生命值不會變負
        m_LiveValue.text = m_Live.ToString();
        if (m_Live <= 0)
        {
            PlayerDie();
        }
    }

    private void PlayerDie()
    {
        m_PlayerLive = false;
    }


    public void OnClick()
    {
        float currentTime = Time.time;
        // 當有點擊時，重置倒數計時器
        countdownTimer = countdownTime;
        isCountingDown = false;

        if (currentTime - lastClickTime <= doubleClickThreshold)
        {
            // 雙擊
            AddScore(2);
            isDoubleClick = true;
        }
        else
        {
            // 單擊
            isDoubleClick = false;
        }

        lastClickTime = currentTime;
    }

    

    public void AddScore(int score)
    {
        if (m_PlayerLive==true)
        {
            // 如果是雙擊，增加兩倍分數
            if (isDoubleClick)
            {
                score += 2;
            }

            m_NowScore += score;
            m_Score.text = m_NowScore.ToString();
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (m_PlayerLive == false)
        {
            return;
        }

        if (!isCountingDown)
        {
            // 如果沒有在倒數，檢查是否需要開始倒數
            bool hasClick = Input.GetMouseButtonDown(0);

            if (!hasClick)
            {
                // 如果沒有點擊，開始倒數計時
                countdownTimer -= Time.deltaTime;

                if (countdownTimer <= 0f)
                {
                    // 開始倒數時，生命值減少
                    m_Live--;

                    if (m_Live < 0)
                    {
                        m_Live = 0;
                    }

                    m_LiveValue.text = m_Live.ToString();

                    if (m_Live <= 0)
                    {
                        PlayerDie();
                    }

                    // 重置倒數計時器
                    countdownTimer = countdownTime;
                }
            }
            else
            {
                // 如果有點擊，增加分數並重置倒數計時器
                AddScore(1); // 單擊增加一分
                countdownTimer = countdownTime;
            }

            // 設定為正在倒數狀態
            isCountingDown = true;
        }
        else
        {
            // 如果正在倒數，檢查是否有點擊，若有則重置倒數計時器
            if (Input.GetMouseButtonDown(0))
            {
                countdownTimer = countdownTime;
            }
        }
    }
}
