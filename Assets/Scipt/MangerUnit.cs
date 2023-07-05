using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MangerUnit : MonoBehaviour
{

    private float m_Time = 0;
    private bool m_IsLive = false;
    private bool m_IsCanClick = false;

    private int m_Hp = 0;
    private float m_RemoveTime = 0;

    public void OnClickMonster()
    {
        if (m_IsCanClick == false)
        {
            return;
        }
        m_Hp --;
        if (m_Hp <= 0)
        {
            GameManger.m_Main.AddScore(1);
            OnDie();
        }

    }
    private void OnDie()
    {
        
        gameObject.SetActive(false);
        m_IsLive = false;
        AddTime();
    }
    void Start()
    {
        AddTime();
        //gameObject.SetActive(true);
    }


    void Update()
    {
      if(m_IsLive)
        {
            if (transform.localScale.x < 1)
            {
                transform.localScale += new Vector3(1f,1f, 1f) * Time.deltaTime;
            }
            else
            {
                m_IsCanClick = true;
                
            }
            if (m_IsCanClick)
            {
                m_RemoveTime += Time.deltaTime;
                int limitTime = 5 - GameManger.m_Main.GetHardRank();
                if (m_RemoveTime > limitTime)
                {
                    GameManger.m_Main.Hit();
                    OnDie();
                }
            }
        }
    }
    public bool CheckTime()
    {
    if (Time.time > m_Time)
            {
                return true;
            }
            return false;
    }
    public void Reburn()
    {
        
        if (m_IsLive ==true)
        {
            return;
        }
        AddTime();
        m_RemoveTime = 0;
        m_Hp = GameManger.m_Main.GetHardRank();
        m_RemoveTime = 0;
        transform.localScale = new Vector3(1f, 1f, 1f);
        m_IsLive = true;
        m_IsCanClick = false;
        gameObject.SetActive(true);

    }
     private void AddTime()
    {
        m_Time = Time.time + Random.Range(0.3f, 0.3f);
    }   
    
}
