using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DPadController : MonoBehaviour
{
    public Player player;
    public float temp;
    public bool m_CanControl;
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
        else if (m_TopMove)
        {
            player.TopMove();
        }
        else if (m_DownMove)
        {
            player.DownMove();
        }
        else if (m_RightMove)
        {
            player.RightMove();
        }
        else
        {
            player.Idle();
        }
    }

    public void LeftGetBottonDown()
    {
            m_LeftMove = true;
    }
    public void RightGetBottonDown()
    {
            m_RightMove = true;
    }
    public void TopGetBottonDown()
    {
            m_TopMove = true;
    }
    public void DownGetBottonDown()
    {
            m_DownMove = true;
    }
    public void LeftGetBottonUp()
    {
            m_LeftMove = false;
    }
    public void RightGetBottonUp()
    {
            m_RightMove = false;
    }
    public void TopGetBottonUp()
    {
            m_TopMove = false;
    }
    public void DownGetBottonUp()
    {
            m_DownMove = false;
    }
}
