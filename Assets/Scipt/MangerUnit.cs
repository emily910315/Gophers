using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MangerUnit : MonoBehaviour
{

    private float m_Time = 0;
    private bool m_IsLive = false;
    private bool m_IsCanClick = false;

    private int m_Hp;//生命值
    private float m_RemoveTime = 0;
    private int m_MaxHp;
    private Dictionary<MangerUnit, int> m_MaxHpDict = new Dictionary<MangerUnit, int>();


    public void OnClickMonster()
    {
        if (m_IsCanClick == false)
        {
            return;
        }
        m_Hp --;
        if (m_Hp <= 0)
        {
            int score = (GameManger.m_Main.GetHardRank() == 1) ? 1 : 2;
            GameManger.m_Main.AddScore(score);
            OnDie();
        }

    }
    private void OnDie()
    {
        StartCoroutine(FadeOut());
        gameObject.SetActive(false);
        m_IsLive = false;
        AddTime();
    }

    private IEnumerator FadeOut()
    {
        float duration = 1.0f;
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration); // 計算透明度的插值
            Color newColor = new Color(1f, 1f, 1f, alpha); // 根據透明度創建新的顏色
            gameObject.GetComponent<Renderer>().material.color = newColor; // 將物體的顏色設定為新的顏色

            elapsedTime += Time.deltaTime; // 更新已過去的時間
            yield return null; // 等待下一幀
        }

        gameObject.SetActive(false); // 停用物體（隱藏物體）
        m_IsLive = false; // 設定為非存活狀態
        AddTime(); // 呼叫 AddTime 方法
    }
    void Start()
    {
        AddTime();
        m_Hp = m_MaxHp; // 設定地鼠的當前生命值為最大生命值
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

        // 創建一個新的地鼠物件
        GameObject newUnitObj = Instantiate(gameObject);
        MangerUnit newUnit = newUnitObj.GetComponent<MangerUnit>();

        // 設定新地鼠物件的最大生命值
        newUnit.m_MaxHp = m_MaxHp;

        m_RemoveTime = 0;
        //m_Hp = GameManger.m_Main.GetHardRank();
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
