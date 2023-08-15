using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ManagerUnit : MonoBehaviour
{

    private float m_Time = 0;
    private bool m_IsLive = false;
    private bool m_IsCanClick = false;

    private float m_Hp = 0;//生命值
    private float m_RemoveTime = 0;
    private int m_MaxHp;
    private Dictionary<ManagerUnit, int> m_MaxHpDict = new Dictionary<ManagerUnit, int>();

    private TextMeshPro m_HitTimes = null;

    private void Awake()
    {
        m_HitTimes = gameObject.GetComponentInChildren<TextMeshPro>();
    }

    public void OnClickMonster()
    {
        gameObject.SetActive(false);
        if (m_IsCanClick == false)
        {
            return;
        }
        m_Hp--;

        m_HitTimes.text = m_Hp.ToString();
        if (m_Hp <= 0)
        {
            int score = (GameManager.Instance.GetHardRank() == 1) ? 1 : 2;
            GameManager.Instance.AddScore(score);
            OnDie();
        }
        m_IsLive = false;
    }
    void Start()
    {
        AddTime();
        m_Hp = m_MaxHp; // 設定地鼠的當前生命值為最大生命值
    }

    void Update()
    {
        if (Time.time > m_Time)
        {
            AddTime();
            gameObject.SetActive(false);
        }

        if (m_IsLive)
        {
            if (transform.localScale.x < 1)
            {
                transform.localScale += new Vector3(1f, 1f, 1f) * Time.deltaTime;
            }
            else
            {
                m_IsCanClick = true;

            }

            if (m_IsCanClick)
            {
                m_RemoveTime += Time.deltaTime;
                int limitTime = 5 - GameManager.Instance.GetHardRank();
                if (m_RemoveTime > limitTime)
                {
                    GameManager.Instance.Hit();
                    OnDie();
                }
            }
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
        float elapsedTime = 0.01f;

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

    public bool CheckTime()
    {
        if (Time.time > m_Time)
        {
            //m_Time = Random.Range(0.01f, 3f);
            return true;
        }
        return false;
    }

    public void Reburn()
    {
        if (m_IsLive)
            return;

        Debug.Log("Reburn() called from: " + transform.name);
        AddTime();

        // 創建一個新的地鼠物件
        //GameObject newUnitObj = Instantiate(gameObject);
        //ManagerUnit newUnit = newUnitObj.GetComponent<ManagerUnit>();

        // 設定新地鼠物件的最大生命值
        //newUnit.m_MaxHp = m_MaxHp;

        m_RemoveTime = 0;
        m_Hp = GameManager.Instance.GetHardRank();
        //m_HitTimes.text = m_Hp.ToString();
        //transform.localScale = new Vector3(1f, 1f, 1f);
        m_IsLive = true;
        m_IsCanClick = false;
        //newUnitObj.SetActive(true);
        gameObject.SetActive(true);

    }

    private void AddTime()
    {
        m_Time = Time.time + Random.Range(0.01f, 0.10f);
    }

}
