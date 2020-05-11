using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerGuageUI : MonoBehaviour
{
    public Image m_HPgauge; // HP graph
    public Image m_MPgauge; // MP graph
    public Text m_HPText;   // HP Text
    public Text m_MPText;   // MP Text
    public Text m_EXP;      // EXP Text
    public GameObject m_LevelUp;      // EXP Text
    public Player m_Player;
    private float m_CurHP;  // member var current HP
    private float m_MaxHP;  // member var max HP
    private float m_CurMP;  // member var current MP
    private float m_MaxMP;  // member var max MP
    private float m_curEXP;  // member var current EXP
    private float m_MaxEXP;  // member var max EXP
    private float m_Timer;
    private bool m_TimerCheck;

    void Start()
    {
        // init data
        m_CurHP = GameManager.Instance.g_PlayerCurrentHP;
        m_MaxHP = GameManager.Instance.g_PlayerHP;
        m_CurMP = GameManager.Instance.g_PlayerCurrentMP;
        m_MaxMP = GameManager.Instance.g_PlayerMP;
    }

    // Update is called once per frame
    void Update()
    {
        // update data
        m_CurHP = GameManager.Instance.g_PlayerCurrentHP;
        m_MaxHP = GameManager.Instance.g_PlayerHP;
        m_CurMP = GameManager.Instance.g_PlayerCurrentMP;
        m_MaxMP = GameManager.Instance.g_PlayerMP;

        // text print
        m_HPText.text = "" + m_CurHP + " / " + m_MaxHP;
        m_MPText.text = "" + m_CurMP + " / " + m_MaxMP;

        // gauge fill amount setting
        m_HPgauge.fillAmount = Mathf.Lerp(m_HPgauge.fillAmount, m_CurHP / m_MaxHP, Time.deltaTime);
        m_MPgauge.fillAmount = Mathf.Lerp(m_MPgauge.fillAmount, m_CurMP / m_MaxMP, Time.deltaTime);

        // EXP Text print
        m_EXP.text = "EXP : " + (GameManager.Instance.g_PlayerEXP - GameManager.Instance.maxLevelUpEXP[GameManager.Instance.g_PlayerLevel - 1])
            + " / " + GameManager.Instance.maxLevelUpEXP[GameManager.Instance.g_PlayerLevel];

        if(m_Player.m_LevelUp)
        {
            m_LevelUp.SetActive(true);
            m_TimerCheck = true;
        }

        if (m_TimerCheck)
        {
            m_Timer += Time.deltaTime;
            if (m_Timer > 2.0f)
            {
                m_LevelUp.SetActive(false);
                m_TimerCheck = false;
                m_Timer = 0;
                m_Player.m_LevelUp = false;
            }
        }
    }
}
