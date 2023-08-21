using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //檢查可否找到GameManager
    private static GameManager m_instance = null;
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


    [SerializeField] Text m_HpText = null;//生命值文字
    [SerializeField] Text m_ScoreText = null;//分數文字
    [SerializeField] ManagerUnit[] m_AllUnit = null;//所有的按紐

    private int _m_Hp = 5;//生命值預設為5
    private int m_Hp
    {
        get
        {
            return _m_Hp;
        }
        set
        {
            _m_Hp = value;
            m_HpText.text = _m_Hp.ToString();//更新生命值
        }
    }

    //private int _m_Score = 0;//分數預設為0
    //private int m_Score
    //{//分數更新
    //    get
    //    {
    //        return _m_Score;
    //    }
    //    set
    //    {
    //        _m_Score = value;
    //        m_ScoreText.text = _m_Score.ToString();//顯示更新分數
    //    }
    //}


    private float countdownTime = 5f; // 倒數的初始時間
    private float countdownTimer = 0f; // 倒數計時器
    
    private bool isCountingDown = false; // 正在倒數

    private bool m_PlayerLive = true;//玩家存活    


    //private float lastClickTime = 0f; // 上次點擊的時間
    //private float doubleClickThreshold = 0.3f; //雙擊值域
    //private bool isDoubleClick = false; // 是否是雙擊


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
            //檢查所有按鈕產生時間
            if (unit.CheckTime())
            {
                //觸發事件條件
                unit.SwitchActive();
            }
            else
            {

            }


        }

        if (!m_PlayerLive)
            return;

        if (!isCountingDown)//檢查是否需要開始倒數
        {   
            //檢查有無開始點擊
            if (!Input.GetMouseButtonDown(0))
            {   
                countdownTimer -= Time.deltaTime;//開始倒數計時
                isCountingDown = true;//正在倒數               
                

                //檢查倒數計時器開始運轉
                if (countdownTimer <= 0f)
                {
                    this.m_Hp -= 1;//生命值-1

                    //檢查生命值是否減少至0
                    if (this.m_Hp == 0)
                    {
                        m_PlayerLive = false;//玩家死亡
                    }

                    else 
                    {
                        countdownTimer = countdownTime;//重置倒數計時器
                    }

                }
            }
                else 
                {
                    //AddScore(1); // 單擊增加一分
                    countdownTimer = countdownTime;//重置倒數計時器
                }
            }
        else
        {
            countdownTimer = countdownTime;//重置倒數計時器
        }
    }

    private void StartCountdown()
    {
        //開始倒數計時的協程
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown()
    {
        //協程
        for (; this.m_Hp > 0; this.m_Hp--)
        {
            yield return new WaitForSeconds(1f); // 等待1秒
        }
            

        m_PlayerLive = false;
}

    public void OnClick()
    {
        // 當有點擊時       
        if (Input.GetMouseButtonDown(1))
        {
            isCountingDown = false; // 停止倒數
            countdownTimer = countdownTime;//重置倒數計時器
        }
        
        
        
        
        //float currentTime = Time.time;

        //    /*if (currentTime - lastClickTime <= doubleClickThreshold)
        //    {
        //        // 雙擊
        //        // AddScore(2);
        //        isDoubleClick = true;
        //    }
        //    else
        //    {
        //        // 單擊
        //        AddScore(1);
        //        isDoubleClick = false;
        //    }*/

        //    //lastClickTime = currentTime;
        //}

        ///*public int GetHardRank()
        //{
        //    return m_HankRank;
        //}*/





        //public void AddScore(int pointsToAdd)
        //{
        //    if (!m_PlayerLive)//玩家是否活著
        //        return;

        //    /*if (m_HankRank == 1 && m_Score > 5)
        //    {
        //        m_HankRank = 2;
        //    }
        //    else if(m_HankRank == 2 && m_Score > 30)
        //    {
        //        m_HankRank = 3;
        //    }
        //    // 如果是雙擊，多加兩分
        //    if (isDoubleClick)
        //        n += 2;*/
        //    m_Score += pointsToAdd; // 增加分數
        //    m_ScoreText.text = m_Score.ToString(); // 更新分數
        //    //this.m_Score += n;
    }
}


