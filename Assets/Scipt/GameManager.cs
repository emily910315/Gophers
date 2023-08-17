using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{//檢查可否找到GameManager
    private static GameManager m_instance = null;
    public static GameManager Instance
    {
        get
        {//無找到則創建一個GameManager
            if (m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }


    public Text m_HpText = null;//生命值文字
    public Text m_ScoreText = null;//分數文字
    public ManagerUnit[] m_AllUnit = null;//所有的按紐

    private int _m_Hp = 5;//生命值預設為5
    private int m_Hp
    {//生命值更新
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
    {//分數更新
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

    private bool m_PlayerLive = true;//玩家存活
    //private int m_HankRank = 1;//玩家等級1

    private bool isCountingDown = false; // 是否正在倒數
    private float countdownTime = 5f; // 倒數的初始時間
    private float countdownTimer = 0f; // 倒數計時器
    private float lastClickTime = 0f; // 上次點擊的時間
    //private float doubleClickThreshold = 0.3f; //雙擊值域
    //private bool isDoubleClick = false; // 是否是雙擊

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
            {//檢查按鈕重新生成時間，若到達則重新生成
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

            //檢查玩家有無開始點擊
            if (!hasClick)
            {
                // 如果沒有點擊，開始倒數計時
                countdownTimer -= Time.deltaTime;

                // 開始倒數時，生命值減少
                if (countdownTimer <= 0f)
                {
                    this.m_Hp--;

                    //生命值減少至0則玩家死亡
                    if (this.m_Hp == 0)
                    {
                        PlayerDie();
                    }
                        
                    // 若生命值在為未減少為0時有點擊，則重置倒數計時器
                    else
                    {
                        
                        countdownTimer = countdownTime;
                    }
                }
            }
            else
            {
                // 如果有點擊
                AddScore(1); // 單擊增加一分
                countdownTimer = countdownTime;//重置倒數計時器
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
    {//開始倒數計時的協程
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {//協程
        for (; this.m_Hp > 0; this.m_Hp--)
            yield return new WaitForSeconds(1f); // 等待1秒

        PlayerDie();
    }

    public void OnClick()
    {
        this.m_Hp = 5;
        //float currentTime = Time.time;
        // 當有點擊時，重置倒數計時器
        countdownTimer = countdownTime;//重製倒數時間
        isCountingDown = false;//點擊後停止倒數

        /*if (currentTime - lastClickTime <= doubleClickThreshold)
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
        }*/

        //lastClickTime = currentTime;
    }

    /*public int GetHardRank()
    {
        return m_HankRank;
    }*/

    public void Hit()
    {//處理按鈕點擊時的狀況

        this.m_Hp -= 1;//生命值-1

        if (this.m_Hp == 0)
            PlayerDie();//玩家死亡
    }

    private void PlayerDie()
    {
        m_PlayerLive = false;//玩家死亡
    }

    public void AddScore(int pointsToAdd)
    {
        if (!m_PlayerLive)//玩家是否活著
            return;

        /*if (m_HankRank == 1 && m_Score > 5)
        {
            m_HankRank = 2;
        }
        else if(m_HankRank == 2 && m_Score > 30)
        {
            m_HankRank = 3;
        }
        // 如果是雙擊，多加兩分
        if (isDoubleClick)
            n += 2;*/
        m_Score += pointsToAdd; // 增加分數
        m_ScoreText.text = m_Score.ToString(); // 更新分數
        //this.m_Score += n;
    }

}
