using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField] ManagerUnit[] m_AllUnits = null; //所有的按紐
    [SerializeField] GameObject UnitsGrid = null;


    private static int MAX_HP = 10;//生命值預設最大為5
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

    private int _m_Score = 5;//分數預設為0
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

    [SerializeField] GameObject bg;
    [SerializeField] GameObject instruction;
    [SerializeField] GameObject win;


    void gamemeunopen()
    {
        Time.timeScale = 0f;
        bg.SetActive(true);
    }

    public void gamemeunclose()
    {
        Time.timeScale = 1f;
        bg.SetActive(false);
    }

    public void instructionopen()
    {
        Time.timeScale = 0f;
        bg.SetActive(false);
        instruction.SetActive(true);
    }

    public void instructionclose()
    {
        Time.timeScale = 1f;
        bg.SetActive(false);
        instruction.SetActive(false);
    }

    public void restart()
    {
        win.SetActive(false);
        SceneManager.LoadScene("SampleScene");
    }

    void Start()
    {
        m_AllUnit = UnitsGrid.GetComponentsInChildren<ManagerUnit>();

        m_AllUnits = UnitsGrid.GetComponentsInChildren<ManagerUnit>();
        m_PlayerLive = true;
        // 倒計時
        StartCountdown();
    }

    void Update()
    {

            for (int i = 0; i < m_AllUnits.Length; i++)
            {
            ManagerUnit unit = m_AllUnits[i];
            if (unit.CheckTime())
            {
                //檢查按鈕重新生成時間，若到達則重新生成
                unit.SwitchActive();
            }
        }

        if (!m_PlayerLive)
        {
            gameObject.SetActive(false);//不再繼續生成按鈕
        }
            
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

    public void UpdateScore(int score)
    {
        if (!m_PlayerLive)
            return;

        m_Score += score;
        m_ScoreText.text = m_Score.ToString();


        for (int i = 0; i < m_AllUnit.Length; i++)
        {
            ManagerUnit unit = m_AllUnit[i];
            if (m_Score < 0)
            {
                gameObject.SetActive(false);// 遊戲結束
            }
        }

    }

}



