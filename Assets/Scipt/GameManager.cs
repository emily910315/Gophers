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
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    public Text m_HpText = null;
    public Text m_ScoreText = null;
    public ManagerUnit[] m_AllUnit = null;

    private int _m_Hp = 5;
    private int m_Hp
    {
        get
        {
            return _m_Hp;
        }
        set
        {
            _m_Hp = value;
            m_HpText.text = _m_Hp.ToString();
        }
    }

    private int _m_Score = 0;
    private int m_Score
    {
        get
        {
            return _m_Score;
        }
        set
        {
            _m_Score = value;
            m_ScoreText.text = _m_Score.ToString();
        }
    }

    private bool m_PlayerLive = true;
    private int m_HankRank = 1;

    private bool isCountingDown = false; // 是否正在倒數
    private float countdownTime = 5f; // 倒數的初始時間
    private float countdownTimer = 0f; // 倒數計時器
    private float lastClickTime = 0f; // 上次點擊的時間
    private float doubleClickThreshold = 0.3f; //雙擊值域
    private bool isDoubleClick = false; // 是否是雙擊

    public void Awake()
    {
        m_instance = this;
    }
    void Start()
    {
        // 倒計時
        StartCountdown();
    }

    void Update()
    {
        for (int i = 0; i < m_AllUnit.Length; i++)
        {
            ManagerUnit unit = m_AllUnit[i];
            if (unit.CheckTime() == true)
            {
                unit.Reburn();
            }
            else
            {

            }
        }

        if (!m_PlayerLive)
            return;

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
                    m_Hp--;

                    if (m_Hp == 0)
                        PlayerDie();

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
    private void StartCountdown()
    {
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        for (; m_Hp > 0; m_Hp--)
            yield return new WaitForSeconds(1f); // 等待1秒

        PlayerDie();
    }

    public void OnClick()
    {
        m_Hp = 5;
        float currentTime = Time.time;
        // 當有點擊時，重置倒數計時器
        countdownTimer = countdownTime;
        isCountingDown = false;

        if (currentTime - lastClickTime <= doubleClickThreshold)
        {
            // 雙擊
            // AddScore(2);
            isDoubleClick = true;
        }
        else
        {
            // 單擊
            AddScore(1);
            isDoubleClick = false;
        }

        lastClickTime = currentTime;
    }

    public int GetHardRank()
    {
        return m_HankRank;
    }

    public void Hit()
    {
        m_Hp -= 1;//生命值扣1

        if (m_Hp == 0)
            PlayerDie();
    }

    private void PlayerDie()
    {
        m_PlayerLive = false;
    }

    public void AddScore(int n)
    {
        if (!m_PlayerLive)
            return;
        if (m_HankRank == 1 && m_Score > 5)
        {
            m_HankRank = 2;
        }
        else if (m_HankRank == 2 && m_Score > 30)
        {
            m_HankRank = 3;
        }
        // 如果是雙擊，多加兩分
        if (isDoubleClick)
            n += 2;

        this.m_Score += n;
    }

}
