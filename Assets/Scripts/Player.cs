using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_Speed;                   // player m_Speed
    public float m_ManaCoolTime;            // mana regene speed
    public Animator m_Anim;                 // player animator
    public Vector3 m_Vector;                // player move vector
    public GameObject m_SkillDamage;        // skill damage collider
    public GameObject m_LackOfManaText;     // not enough mana text
    public GameObject m_SkillParticle;      // skill effect
    public GameObject m_ControlPad;         // control pad 
    public GameObject m_Skill1Obj;          // skill 1 Obj
    public GameObject m_Skill2Obj;          // skill 1 Obj
    public GameObject m_UltimateObj;        // skill 1 Obj  
    public bool m_IsPlayerField;            // where is player? 0 - Village, 1 - Field
    public bool m_CanMove;                  // can move or not
    public bool m_CanAtk;                   // can attack or not
    public RaycastHit2D rayHit;             // attack ray
    private Vector3 m_DirVec;               // attack ray direction
    private float m_CurTime;                // manaregen current time
    private Enemy enemy;                    // enemy

    void Start()
    {
        // init data
        m_CanMove = true;
        m_CanAtk = true;
        enemy = GetComponent<Enemy>();
        GameManager.Instance.g_PlayerAtk = GameManager.Instance.g_PlayerStr * 2;
    }

    void Update()
    {
        // mana regenerate
        if (GameManager.Instance.g_PlayerCurrentMP != GameManager.Instance.g_PlayerMP)
        {
            m_CurTime += Time.deltaTime;

            if (m_CurTime > m_ManaCoolTime)
            {
                GameManager.Instance.g_PlayerCurrentMP++;
                m_CurTime = 0;
            }
        }

        m_Anim.SetBool("IsChange", false);

        // player idle check
        if (m_Anim.GetInteger("HorizontalMove") == 0 && m_Anim.GetInteger("VerticalMove") == 0)
        {
            m_Anim.SetBool("Idle", true);
        }
        else
        {
            m_Anim.SetBool("Idle", false);
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

        // if text activate, it will destory
        if(m_LackOfManaText.activeSelf)
        {
            Invoke("NotEnoughManaDestroy", 1.0f);
        }

        Debug.Log(GameManager.Instance.g_PlayerLevel);

        // test code for event manager
        if(Input.GetKeyDown(KeyCode.D))
        {
            EventManager.Instance.PlayerMoveRight(5.0f);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            EventManager.Instance.PlayerMoveLeft(5.0f);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            EventManager.Instance.PlayerMoveUp(5.0f);
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            EventManager.Instance.PlayerMoveDown(5.0f);
        }
        // ******** not need func (for test - When it open, it should be delete)
        // move vertical and horizontal
        //if (m_CanMove)
        //{
        //    if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        //    {
        //        m_Vector.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

        //        if (m_Vector.x != 0)
        //        {
        //            transform.Translate(m_Vector.x * m_Speed, 0, 0);
        //        }
        //        else if (m_Vector.y != 0)
        //        {
        //            transform.Translate(0, m_Vector.y * m_Speed, 0);
        //        }
        //    }
        //    else
        //    {
        //        m_Vector.Set(0, 0, 0);
        //    }
        //}
        // ********* until here need to delete

    }

    // action func
    public void Action()
    {
        if(m_IsPlayerField)
        {
            // Field function
            m_CanMove = false;
            m_Anim.SetTrigger("Atk");
        }
        else
        {
            // Village function

        }
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
            if(rayHit.collider.gameObject.GetComponent<Enemy>().m_EnemyCurHP <= 0)
            {
                GameManager.Instance.g_PlayerEXP += rayHit.collider.gameObject.GetComponent<Enemy>().m_EXP;
                if(GameManager.Instance.g_PlayerEXP > 100)
                {
                    GameManager.Instance.g_PlayerLevel++;
                }
            }
        }
    }

    

    // player moving control
    public void CanMove()
    {
        m_CanMove = true;
    }
    public void CanNotMove()
    {
        m_CanMove = false;
    }

    // player D pad control
    public void CanControl()
    {
        m_ControlPad.SetActive(true);
    }
    public void CanNotControl()
    {
        m_ControlPad.SetActive(false);
    }

    // notenoughtmana text destroy
    void NotEnoughManaDestroy()
    {
        m_LackOfManaText.SetActive(false);
    }

    // skill 1 func
    public void Skill1()
    {
        // active skill 1
        if (GameManager.Instance.g_PlayerCurrentMP > 30)
        {
            m_SkillDamage.SetActive(true);
            Instantiate(m_SkillParticle, gameObject.transform.position, Quaternion.identity);
            m_Anim.SetTrigger("SkillAtk");
        }
        else
        {
            m_LackOfManaText.SetActive(true);
        }
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
        if (m_CanMove)
        {
            if (m_Anim.GetInteger("HorizontalMove") != -1)
            {
                m_Anim.SetBool("IsChange", true);
                m_Anim.SetInteger("HorizontalMove", -1);
                m_Anim.SetInteger("VerticalMove", 0);
            }
            else
            {
                m_Anim.SetBool("IsChange", false);
            }

            m_Vector.Set(-1, 0, 0);

            if (m_Vector.x != 0)
            {
                transform.Translate(m_Vector.x * m_Speed, 0, 0);
            }
            else if (m_Vector.y != 0)
            {
                transform.Translate(0, m_Vector.y * m_Speed, 0);
            }
        }
    }

    public void TopMove()
    {
        m_DirVec = Vector3.up;
        if (m_CanMove)
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
                m_Anim.SetInteger("HorizontalMove", 0);
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
        if (m_CanMove)
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
                m_Anim.SetInteger("HorizontalMove", 0);
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
        if (m_CanMove)
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
                m_Anim.SetInteger("VerticalMove", 0);
            }
            else
            {
                m_Anim.SetBool("IsChange", false);
            }
        }
    }

    public void Idle()
    {
         m_Vector.Set(0, 0, 0);
         
         if (m_Anim.GetInteger("HorizontalMove") != 0)
         {
             m_Anim.SetBool("IsChange", true);
             m_Anim.SetInteger("HorizontalMove", 0);
             m_Anim.SetInteger("VerticalMove", 0);
         }
         else
         {
             m_Anim.SetBool("IsChange", false);
         }
    }
}