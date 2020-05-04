using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovingObject : MonoBehaviour
{
    public float speed;
    public Animator m_Anim;

    public Vector3 vector;

    void Start()
    {

    }

    public void LeftMove()
    {
        vector.Set(-1, 0, 0);
        m_Anim.SetBool("IsChange", true);
        m_Anim.SetInteger("HorizontalMove", -1);
       
        if (vector.x != 0)
        {
            transform.Translate(vector.x * speed, 0, 0);
        }
        else if (vector.y != 0)
        {
            transform.Translate(0, vector.y * speed, 0);
        }
    }

    public void TopMove()
    {
        vector.Set(0, 1, 0);
        m_Anim.SetBool("IsChange", true);
        m_Anim.SetInteger("VerticalMove", 1);
        if (vector.x != 0)
        {
            transform.Translate(vector.x * speed, 0, 0);
        }
        else if (vector.y != 0)
        {
            transform.Translate(0, vector.y * speed, 0);
        }
    }

    public void DownMove()
    {
        vector.Set(0, -1, 0);
        m_Anim.SetBool("IsChange", true);
        m_Anim.SetInteger("VerticalMove", -1);
        if (vector.x != 0)
        {
            transform.Translate(vector.x * speed, 0, 0);
        }
        else if (vector.y != 0)
        {
            transform.Translate(0, vector.y * speed, 0);
        }
    }

    public void RightMove()
    {
        vector.Set(1, 0, 0);
        m_Anim.SetBool("IsChange", true);
        m_Anim.SetInteger("HorizontalMove", 1);
        if (vector.x != 0)
        {
            transform.Translate(vector.x * speed, 0, 0);
        }
        else if (vector.y != 0)
        {
            transform.Translate(0, vector.y * speed, 0);
        }
    }

    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if (vector.x != 0)
            {
                transform.Translate(vector.x * speed, 0, 0);
            }
            else if (vector.y != 0)
            {
                transform.Translate(0, vector.y * speed, 0);
            }
        }
        else
        {
            vector.Set(0, 0, 0);
        }

        // player idle check
        if (vector.x == 0 && vector.y == 0)
        {
            m_Anim.SetBool("Idle", true);
        }
        else
        {
            m_Anim.SetBool("Idle", false);
        }

        // player moving animation
        if (m_Anim.GetInteger("HorizontalMove") != vector.x)
        {
            m_Anim.SetBool("IsChange", true);
            m_Anim.SetInteger("HorizontalMove", (int)vector.x);
        }
        else if (m_Anim.GetInteger("VerticalMove") != vector.y)
        {
            m_Anim.SetBool("IsChange", true);
            m_Anim.SetInteger("VerticalMove", (int)vector.y);
        }
        else
        {
            m_Anim.SetBool("IsChange", false);
        }
    }
}

