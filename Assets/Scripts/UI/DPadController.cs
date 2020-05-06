using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DPadController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Player player;
    public float temp;
    public bool m_IsCanMove = true;
    public bool m_LeftMove;
    public bool m_TopMove;
    public bool m_DownMove;
    public bool m_RightMove;

    // Start is called before the first frame update
    void Start()
    {
        player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (m_LeftMove)
        {
            player.LeftMove();
        }
        if (m_TopMove)
        {
            player.TopMove();
        }
        if (m_DownMove)
        {
            player.DownMove();
        }
        if (m_RightMove)
        {
            player.RightMove();
        }
    }

    public void LeftGetBottonDown()
    {
        if (m_IsCanMove)
        {
            m_LeftMove = true;
        }
    }
    public void RightGetBottonDown()
    {
        if (m_IsCanMove)
        {
            m_RightMove = true;
        }
    }
    public void TopGetBottonDown()
    {
        if (m_IsCanMove)
        {
            m_TopMove = true;
        }
    }
    public void DownGetBottonDown()
    {
        if (m_IsCanMove)
        {
            m_DownMove = true;
        }
    }
    public void LeftGetBottonUp()
    {
        if (m_IsCanMove)
        {
            m_LeftMove = false;
        }
    }
    public void RightGetBottonUp()
    {
        if (m_IsCanMove)
        {
            m_RightMove = false;
        }
    }
    public void TopGetBottonUp()
    {
        if (m_IsCanMove)
        {
            m_TopMove = false;
        }
    }
    public void DownGetBottonUp()
    {
        if (m_IsCanMove)
        {
            m_DownMove = false;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        m_IsCanMove = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        m_IsCanMove = false;
    }
}
