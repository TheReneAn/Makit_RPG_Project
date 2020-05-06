using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_Speed;       // player m_Speed
    public Animator m_Anim;     // player animator
    public Vector3 m_Vector;    // player move vector

    public bool m_CanMove;
    public bool m_NotMove;
    public bool m_CanAtk;
    public RaycastHit2D rayHit;
    private Vector3 m_DirVec;
    private float m_CurTime;
    private float m_CoolTime = 0.5f;
    private Enemy enemy;

    void Start()
    {
        // init data
        m_CanMove = true;
        m_NotMove = false;
        m_CanAtk = true;
        enemy = GetComponent<Enemy>();
        GameManager.Instance.g_PlayerAtk = GameManager.Instance.g_PlayerStr * 2;
    }

    void Update()
    {
        // player idle check
        if (m_Vector.x == 0 && m_Vector.y == 0)
        {
            m_Anim.SetBool("Idle", true);
        }
        else
        {
            m_Anim.SetBool("Idle", false);
        }

        // not need func (for test)
        // move vertical and horizontal
        if (m_CanMove && !m_NotMove)
        {
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                m_Vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

                if (m_Vector.x != 0)
                {
                    transform.Translate(m_Vector.x * m_Speed, 0, 0);
                }
                else if (m_Vector.y != 0)
                {
                    transform.Translate(0, m_Vector.y * m_Speed, 0);
                }
            }
            else
            {
                m_Vector.Set(0, 0, 0);
            }
        }

        // player moving animation
        if (m_Anim.GetInteger("HorizontalMove") != m_Vector.x)
        {
            m_Anim.SetBool("IsChange", true);
            m_Anim.SetInteger("HorizontalMove", (int)m_Vector.x);
        }
        else if (m_Anim.GetInteger("VerticalMove") != m_Vector.y)
        {
            m_Anim.SetBool("IsChange", true);
            m_Anim.SetInteger("VerticalMove", (int)m_Vector.y);
        }
        else
        {
            m_Anim.SetBool("IsChange", false);
        }

        // draw ray direction
        if (m_Vector.x == 1)
        {
            m_DirVec = Vector3.right;
        }
        else if (m_Vector.x == -1)
        {
            m_DirVec = Vector3.left;
        }
        else if (m_Vector.y == 1)
        {
            m_DirVec = Vector3.up;
        }
        else if (m_Vector.y == -1)
        {
            m_DirVec = Vector3.down;
        }
    }

    // action func
    public void Action()
    {
        m_CanMove = false;
        m_Anim.SetTrigger("Atk");
    }

    public void Move()
    {
        m_NotMove = false;
    }

    public void NotMove()
    {
        m_NotMove = true;
    }

    public void AttackRay()
    {
        // raycasy physics
        rayHit = Physics2D.Raycast(transform.position, m_DirVec, 1.2f, LayerMask.GetMask("Enemy"));

        if (rayHit)
        {
            // enemy hit knock back
            rayHit.collider.gameObject.transform.position =
            new Vector2(rayHit.collider.gameObject.transform.position.x + m_DirVec.x / 2,
            rayHit.collider.gameObject.transform.position.y + m_DirVec.y / 2);

            // enemy damage
            rayHit.collider.gameObject.GetComponent<Enemy>().IsHit();
        }
    }

    public void CanMove()
    {
        m_CanMove = true;
    }

    // skill 1 func
    public void Skill1()
    {
    }

    // skill 2 func
    public void Skill2()
    {

    }

    // final skill func
    public void UltimateSkill()
    {

    }

    // d-pad moving functions
    public void LeftMove()
    {
        m_DirVec = Vector3.left;
        if (m_CanMove && !m_NotMove)
        {
            m_Vector.Set(-1, 0, 0);

            if (m_Vector.x != 0)
            {
                transform.Translate(m_Vector.x * m_Speed, 0, 0);
            }
            else if (m_Vector.y != 0)
            {
                transform.Translate(0, m_Vector.y * m_Speed, 0);
            }

            if (m_Anim.GetInteger("HorizontalMove") != -1)
            {
                m_Anim.SetBool("IsChange", true);
                m_Anim.SetInteger("HorizontalMove", -1);
            }
            else
            {
                m_Anim.SetBool("IsChange", false);
            }
        }
    }

    public void TopMove()
    {
        m_DirVec = Vector3.up;
        if (m_CanMove && !m_NotMove)
        {
            m_Vector.Set(0, 1, 0);

            if (m_Vector.x != 0)
            {
                transform.Translate(m_Vector.x * m_Speed, 0, 0);
            }
            else if (m_Vector.y != 0)
            {
                transform.Translate(0, m_Vector.y * m_Speed, 0);
            }

            if (m_Anim.GetInteger("VerticalMove") != 1)
            {
                m_Anim.SetBool("IsChange", true);
                m_Anim.SetInteger("VerticalMove", 1);
            }
            else
            {
                m_Anim.SetBool("IsChange", false);
            }
        }
    }

    public void DownMove()
    {
        m_DirVec = Vector3.down;
        if (m_CanMove && !m_NotMove)
        {
            m_Vector.Set(0, -1, 0);

            if (m_Vector.x != 0)
            {
                transform.Translate(m_Vector.x * m_Speed, 0, 0);
            }
            else if (m_Vector.y != 0)
            {
                transform.Translate(0, m_Vector.y * m_Speed, 0);
            }

            if (m_Anim.GetInteger("VerticalMove") != -1)
            {
                m_Anim.SetBool("IsChange", true);
                m_Anim.SetInteger("VerticalMove", -1);
            }
            else
            {
                m_Anim.SetBool("IsChange", false);
            }
        }
    }

    public void RightMove()
    {
        m_DirVec = Vector3.right;
        if (m_CanMove && !m_NotMove)
        {
            m_Vector.Set(1, 0, 0);

            if (m_Vector.x != 0)
            {
                transform.Translate(m_Vector.x * m_Speed, 0, 0);
            }
            else if (m_Vector.y != 0)
            {
                transform.Translate(0, m_Vector.y * m_Speed, 0);
            }

            if (m_Anim.GetInteger("HorizontalMove") != 1)
            {
                m_Anim.SetBool("IsChange", true);
                m_Anim.SetInteger("HorizontalMove", 1);
            }
            else
            {
                m_Anim.SetBool("IsChange", false);
            }
        }
    }
}