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
        m_Live -= 1;
        if (m_Live < 0)
        {
            m_Live = 0;
        }
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
    public void AddScore(int score)
    {
        m_NowScore += score;
        if (m_HankRank == 1 && m_NowScore > 5)
        {
            m_HankRank = 1;//打一下
        }
        else if(m_HankRank == 2 && m_NowScore > 30)
        {
            m_HankRank = 2;//打兩下
        }
        m_Score.text = m_NowScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_PlayerLive == false)
        {
            return;
        }
        for(int i = 0; i < m_AllUnit.Length; i++)
        {
            MangerUnit unit = m_AllUnit[i];
            if (unit.CheckTime() == true)
            {
                unit.Reburn();

            }
            else 
            {

            }
            
        }
    }
}
