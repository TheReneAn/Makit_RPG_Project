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

    private float m_CurHP;  // member var current HP
    private float m_MaxHP;  // member var max HP
    private float m_CurMP;  // member var current MP
    private float m_MaxMP;  // member var max MP

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

        // for test
        if(Input.GetKeyDown("]"))
        {
            GameManager.Instance.g_PlayerCurrentHP += 10;
            GameManager.Instance.g_PlayerCurrentMP += 10;
        }
        if (Input.GetKeyDown("["))
        {
            GameManager.Instance.g_PlayerCurrentHP -= 10;
            GameManager.Instance.g_PlayerCurrentMP -= 10;
        }
    }
}
