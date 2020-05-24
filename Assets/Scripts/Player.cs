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
    public GameObject m_SkillDamageCol;        // skill damage collider
    public GameObject m_NormalDamageCol;        // normal damage collider
    public GameObject m_LackOfManaText;     // not enough mana text
    public GameObject m_SkillParticle;      // skill effect
    public GameObject m_LevelUpParticle;    // LevelUp effect
    public GameObject m_ControlPad;         // control pad 
    public GameObject m_Skill1Obj;          // skill 1 Obj
    public GameObject m_Skill2Obj;          // skill 2 Obj
    public GameObject m_UltimateObj;        // Ulti Obj  
    public GameObject m_GameOverTxt;        // Ulti Obj  

    public bool m_IsPlayerField;            // where is player? 0 - Village, 1 - Field
    public bool m_LevelUp;
    public bool m_CanMove;                  // can move or not
    public bool m_CanAtk;                   // can attack or not

    private Vector3 m_DirVec;               // attack ray direction
    private float m_CurTime;                // manaregen current time
    private bool m_Die;                // 
    private Enemy enemy;                    // enemy
    GameObject scanObject;


    void Start()
    {
        // init data
        m_CanMove = true;
        m_CanAtk = true;
        enemy = GetComponent<Enemy>();
        GameManager.Instance.g_PlayerCurrentHP = GameManager.Instance.g_PlayerHP;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogueManager.instance.ShowDialogue(DialogueManager.instance.g_Dialogue[0]);
        }

        // set player status
        SetStatus();

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

        // player moving animation
        Animation();

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
        if (m_LackOfManaText != null)
        {
            if (m_LackOfManaText.activeSelf)
            {
                Invoke("NotEnoughManaDestroy", 1.0f);
            }
        }
        // player level sequence
        if(GameManager.Instance.g_PlayerEXP >= GameManager.Instance.maxLevelUpEXP[GameManager.Instance.g_PlayerLevel])
        {
            GameManager.Instance.g_PlayerLevel++;
            Instantiate(m_LevelUpParticle, gameObject.transform.position, Quaternion.identity);
            m_LevelUp = true;
            GameManager.Instance.g_PlayerAtk += 3;
            GameManager.Instance.g_PlayerDex += 3;
            GameManager.Instance.g_PlayerCon += 3;
            GameManager.Instance.g_PlayerCurrentHP = GameManager.Instance.g_PlayerHP;
            GameManager.Instance.g_PlayerCurrentMP = GameManager.Instance.g_PlayerMP;
        }

        if (GameManager.Instance.g_PlayerCurrentHP <= 0)
        {
            if (!m_Die)
            {
                GameManager.Instance.g_PlayerCurrentHP = 0;
                m_ControlPad.SetActive(false);
                m_GameOverTxt.SetActive(true);
                m_Anim.SetBool("IsDie", true);
                m_Anim.SetBool("IsChange", true);
                m_Anim.SetBool("Idle", false);
                m_Die = true;
            }
            else
            {
                GameManager.Instance.g_PlayerCurrentHP = 0;
                m_Anim.SetBool("IsChange", false);
                m_Anim.SetBool("Idle", false);
                Invoke("GameOver", 5);
            }
        }

        // collide obstacle
        int layerMask = 1 << LayerMask.NameToLayer("Obstacle");
        if(Physics2D.Raycast(transform.position, m_DirVec, 20, layerMask))
        {
            Idle();
            CanNotMove();
        }
        else
        {
            CanMove();
        }
    }


    public void Animation()
    {
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
    }
    void GameOver()
    {
        GameManager.Instance.SceneChange("GameOver");
    }

    // action func
    public void Action()
    {
        if (m_IsPlayerField)
        {
            // Field function
            m_CanMove = false;
            m_Anim.SetTrigger("Atk");
        }
        else
        {
            // Village function
            //Debug.DrawRay(transform.position, m_DirVec * 30, Color.red, 0.5f);
            //RaycastHit2D rayHit = Physics2D.Raycast(transform.position, m_DirVec, 0.3f, LayerMask.GetMask("NPC"));

            //if (rayHit.collider != null)
            //{
            //    scanObject = rayHit.collider.gameObject;

            //    Debug.Log("Collide");
            //    scanObject.GetComponent<QuestObj>().CheckQuest();
            //}
            //else
            //{
            //    scanObject = null;
            //}
        }
    }

    public void AttackStart()
    {
        m_NormalDamageCol.SetActive(true);
        m_NormalDamageCol.transform.position = transform.position + m_DirVec * 0.7f;
        transform.Translate(new Vector2(m_DirVec.x * Time.deltaTime * m_Speed, m_DirVec.y * Time.deltaTime * m_Speed));
        //iTween.MoveBy(gameObject, new Vector2(m_DirVec.x * Time.deltaTime * 1000, m_DirVec.y * Time.deltaTime * 1000), 0.5f);

        iTween.MoveBy(gameObject, iTween.Hash("x", m_DirVec.x * Time.deltaTime * 1000, "y", m_DirVec.y * Time.deltaTime * 1000, "time", 0.3f, "easetype", iTween.EaseType.easeOutQuint));
    }

    public void AttackEnd()
    {
        m_NormalDamageCol.SetActive(false);
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
            m_SkillDamageCol.SetActive(true);
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

    void SetStatus()
    {
        GameManager.Instance.g_PlayerAtk = GameManager.Instance.g_PlayerStr; // + item atk
        GameManager.Instance.g_PlayerHP = 100 + GameManager.Instance.g_PlayerCon * 3; // + item hp
        GameManager.Instance.g_PlayerDef = GameManager.Instance.g_PlayerCon / 2; // + item def
        GameManager.Instance.g_PlayerAvd = GameManager.Instance.g_PlayerDex; // + item avd
        GameManager.Instance.g_PlayerCrt = GameManager.Instance.g_PlayerDex; // + item critical
    }
}