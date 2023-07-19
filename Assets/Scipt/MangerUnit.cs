using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MangerUnit : MonoBehaviour
{

    private float m_Time = 0;
    private bool m_IsLive = false;
    private bool m_IsCanClick = false;

    private int m_Hp;//�ͩR��
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
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration); // �p��z���ת�����
            Color newColor = new Color(1f, 1f, 1f, alpha); // �ھڳz���׳Ыطs���C��
            gameObject.GetComponent<Renderer>().material.color = newColor; // �N���骺�C��]�w���s���C��

            elapsedTime += Time.deltaTime; // ��s�w�L�h���ɶ�
            yield return null; // ���ݤU�@�V
        }

        gameObject.SetActive(false); // ���Ϊ���]���ê���^
        m_IsLive = false; // �]�w���D�s�����A
        AddTime(); // �I�s AddTime ��k
    }
    void Start()
    {
        AddTime();
        m_Hp = m_MaxHp; // �]�w�a������e�ͩR�Ȭ��̤j�ͩR��
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

        // �Ыؤ@�ӷs���a������
        GameObject newUnitObj = Instantiate(gameObject);
        MangerUnit newUnit = newUnitObj.GetComponent<MangerUnit>();

        // �]�w�s�a�����󪺳̤j�ͩR��
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
