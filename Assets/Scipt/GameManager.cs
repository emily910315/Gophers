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

    private bool m_PlayerLive;//玩家存活
    private static int MAX_HP = 5;//生命值最大預設為5
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



    //private float lastClickTime = 0f; // 上次點擊的時間
    //private float doubleClickThreshold = 0.3f; //雙擊值域
    //private bool isDoubleClick = false; // 是否是雙擊

    public void Awake()
    {
        m_instance = this;
    }

    void Start()
    {
        //玩家存活
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

<<<<<<< Updated upstream
        if (!m_PlayerLive)
            return;
=======
       
>>>>>>> Stashed changes
    }

    public void ResetHp()//重置生命值並重新計時
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
<<<<<<< Updated upstream
        //協程
        for (; m_Hp > 0; m_Hp--)
=======
        //無點擊後進入迴圈倒數生命值
        for (; this.m_Hp > 0; this.m_Hp--)
>>>>>>> Stashed changes
        {
            yield return new WaitForSeconds(1f); // 等待1秒
        }

<<<<<<< Updated upstream
        m_PlayerLive = false;
=======
        if (this.m_Hp == 0)
        {
            m_PlayerLive = false;//玩家死亡
        }
            
}

    public void OnClick()
    {
        m_Hp();

            isCountingDown = false; // 停止倒數
            countdownTimer = countdownTime;//重置倒數計時器
  
        
        
        
        
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
>>>>>>> Stashed changes
    }
}
