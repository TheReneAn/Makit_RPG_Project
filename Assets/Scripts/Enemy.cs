using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum ENEMYSTATE // Enemy state enum
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    IDLE,
    DIE
}

public class Enemy : MonoBehaviour
{
    public int m_EnemyHP;                // enemy HP
    public int m_EnemyCurHP;             // enemy HP
    public int m_Atk;                    // enemy atk damage
    public int m_EXP;                    // enemy Exp
    public float m_Speed;                // enemy speed
    public float m_Aggro;                // enemy aggro range
    public float m_AtkRange;             // enemy attack range
    public GameObject m_FindImage;       // enemy emphasize image (aggro)
    public Animator m_Anim;
    public GameObject m_HPsprite;
    public GameObject m_HitEffect;
    public int m_SpeciesNum;

    private ENEMYSPECIES m_Species;
    private GameObject m_Player;         // player object
    private Vector2 m_PlayerPos;         // player position
    private int m_RandomInt;             // random moving
    private ENEMYSTATE m_CurState;       // enemy current state;
    private ENEMYSTATE m_PrevState;      // enemy previous state;
    private RaycastHit2D rayHit;
    private Vector3 m_DirVec;
    private Vector3 m_Home;

    private float m_TimeCount;           // time count (behave)
    private bool m_CanMove = true;       // can move bool
    private Vector3 m_Vector;            // enemy move vector
    private float m_EnemyBehaveCoolTime; // enemy behave cooltime

    void Start()
    {
        // init
       m_Player = GameObject.Find("Player");
       m_EnemyBehaveCoolTime = 1;
        m_CurState = ENEMYSTATE.IDLE;
    }

    void Update()
    {
        // set species
        if (m_SpeciesNum == 0)
            m_Species = ENEMYSPECIES.GOBLIN;
        if (m_SpeciesNum == 1)
            m_Species = ENEMYSPECIES.SLIME;
        if (m_SpeciesNum == 2)
            m_Species = ENEMYSPECIES.WOLF;

        // compare before action
        m_PrevState = m_CurState;
        m_Anim.SetBool("IsChange", false); // for animation one shot

        if (m_EnemyCurHP > 0) // if enemy live
        {
            // time count increase
            m_TimeCount += Time.deltaTime;
            // player position check
            m_PlayerPos = m_Player.transform.position;

            if (m_TimeCount > m_EnemyBehaveCoolTime) // behave cooltime
            {
                if (NearPlayer()) // player aggro check
                {
                    m_HPsprite.SetActive(true);  // hp guage on
                    m_FindImage.SetActive(true); // aggro enphasize image - on

                    // attack range check
                    if (AtkPlayer())
                        Attack(); // attack player
                    else
                        ChasePlayer(); // chase player
                }
                else
                {
                    m_FindImage.SetActive(false); // aggro enphasize image - off
                    m_HPsprite.SetActive(false);  // hp guage off
                    if (m_Species == ENEMYSPECIES.GOBLIN)
                        RandomMove();   // random move
                    else if (m_Species == ENEMYSPECIES.WOLF)
                    {
                        m_CurState = ENEMYSTATE.IDLE;
                        m_Anim.SetBool("IsChange", true);
                        ChangeAnimation("Idle", true);
                    }
                }
                m_TimeCount = 0;    // timer init
            }
            EnemyMovingProcess();   // enemy actual movement function

            //enemy idle check
            if (m_Vector.x == 0 && m_Vector.y == 0)
            {
                ChangeAnimation("Idle", true);
            }
            else
            {
                ChangeAnimation("Idle", false);
            }
        }
        else // die
        {
            IsDie();
            Destroy(gameObject, 3);
        }
    }

    // EXP sequence
    void TakeEXP()
    {
        GameManager.Instance.g_PlayerEXP += m_EXP;
    }

    // animation setting (polymorph)
    void ChangeAnimation(string animName, bool tempBool)
    {
        if (m_PrevState != m_CurState)
        {
            m_Anim.SetBool("IsChange", true);
        }
        m_Anim.SetBool(animName, tempBool);
    }

    void ChangeAnimation(string animName, float tempFloat)
    {
        if (m_PrevState != m_CurState)
        {
            m_Anim.SetBool("IsChange", true);
        }
        m_Anim.SetFloat(animName, tempFloat);
    }
    void ChangeAnimation(string animName)
    {
        if (m_PrevState != m_CurState)
        {
            m_Anim.SetBool("IsChange", true);
        }
        m_Anim.SetTrigger(animName);
    }

    // enemy actual movement function
    void EnemyMovingProcess()
    {
        if (m_CanMove)
        {
            switch (m_CurState) // moving state
            {
                case ENEMYSTATE.UP:
                    m_Vector.x = 0f;
                    m_Vector.y = 1f;
                    ChangeAnimation("VerticalMove", 1);
                    ChangeAnimation("HorizontalMove", 0);
                    m_DirVec = Vector3.up;
                    break;
                case ENEMYSTATE.DOWN:
                    m_Vector.x = 0f;
                    m_Vector.y = -1f;
                    ChangeAnimation("VerticalMove", -1);
                    ChangeAnimation("HorizontalMove", 0);
                    m_DirVec = Vector3.down;
                    break;
                case ENEMYSTATE.LEFT:
                    m_Vector.x = -1f;
                    m_Vector.y = 0f;
                    ChangeAnimation("HorizontalMove", -1);
                    ChangeAnimation("VerticalMove", 0);
                    m_DirVec = Vector3.left;
                    break;
                case ENEMYSTATE.RIGHT:
                    m_Vector.x = 1f;
                    m_Vector.y = 0f;
                    ChangeAnimation("HorizontalMove", 1);
                    ChangeAnimation("VerticalMove", 0);
                    m_DirVec = Vector3.right;
                    break;
                default:
                    m_Vector.x = 0;
                    m_Vector.y = 0;
                    break;
            }

            // vector translate
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

    // damaged by player
    public void IsHit()
    {
        m_EnemyCurHP -= GameManager.Instance.g_PlayerAtk;
        m_HPsprite.transform.localScale = new Vector3(((float)m_EnemyCurHP / (float)m_EnemyHP), 
            m_HPsprite.transform.localScale.y, m_HPsprite.transform.localScale.z);
        Instantiate(m_HitEffect, gameObject.transform.position, Quaternion.identity);
    }

    // die sequence
    void IsDie()
    {
        m_HPsprite.SetActive(false);
        m_FindImage.SetActive(false);
        m_CurState = ENEMYSTATE.DIE;
        ChangeAnimation("IsDie", true);
        m_CanMove = false;

        // player get EXP
        if (m_Anim.GetBool("IsChange"))
        {
            TakeEXP();
        }
    }

    // check aggro range
    bool NearPlayer()
    {
        if (Mathf.Abs(m_PlayerPos.x - this.transform.position.x) < m_Aggro && 
            Mathf.Abs(m_PlayerPos.y - this.transform.position.y) < m_Aggro)
        {
            return true;
        }

        return false;
    }

    // check attack range
    bool AtkPlayer()
    {

        if (Mathf.Abs(m_PlayerPos.x - this.transform.position.x) < m_AtkRange &&
            Mathf.Abs(m_PlayerPos.y - this.transform.position.y) < m_AtkRange)
        {
            return true;
        }

        return false;
    }

    // enemy random move (nomal state)
    void RandomMove()
    {
        m_CanMove = true;

        RandomDirection();
        m_TimeCount = 0;
    }

    // enemy chase player (chase state)
    void ChasePlayer()
    {
        // x axis more close to player
        if (Mathf.Abs(m_PlayerPos.x - this.transform.position.x) > 
            Mathf.Abs(m_PlayerPos.y - this.transform.position.y))
        {
            if (m_PlayerPos.x - this.transform.position.x > 0)
            {
                m_CurState = ENEMYSTATE.RIGHT;
            }
            else
            {
                m_CurState = ENEMYSTATE.LEFT;
            }
        }
        // y axis more close to player
        else
        {
            if (m_PlayerPos.y - this.transform.position.y > 0)
            {
                m_CurState = ENEMYSTATE.UP;
            }
            else
            {
                m_CurState = ENEMYSTATE.DOWN;
            }
        }
        m_TimeCount = 0;
    }

    // attack to player
    void Attack()
    {
        m_CanMove = false; // stop move
        float distanceX = 0;
        float distanceY = 0;

        distanceX = m_PlayerPos.x - transform.position.x;
        distanceY = m_PlayerPos.y - transform.position.y;

        if (Mathf.Abs(distanceX) > Mathf.Abs(distanceY))
        {
            if (distanceX > 0)
            {
                ChangeAnimation("Atk");
            }
            else
            {
                ChangeAnimation("Atk");
            }
        }
        else
        {
            if (distanceY > 0)
            {
                ChangeAnimation("Atk");
            }
            else
            {
                ChangeAnimation("Atk");
            }
        }
    }

    public void AttackRay()
    {
        // raycasy physics
        if (m_Species == ENEMYSPECIES.GOBLIN)
            rayHit = Physics2D.Raycast(transform.position, m_DirVec, 1.2f, LayerMask.GetMask("Player"));
        else if (m_Species == ENEMYSPECIES.WOLF)
            rayHit = Physics2D.Raycast(transform.position, m_DirVec, 1.5f, LayerMask.GetMask("Player"));

        if (rayHit)
        {
             // enemy hit knock back
             rayHit.collider.gameObject.transform.position =
             new Vector2(rayHit.collider.gameObject.transform.position.x + m_DirVec.x / 2,
             rayHit.collider.gameObject.transform.position.y + m_DirVec.y / 2);
        }

        // player damaged
        GameManager.Instance.g_PlayerCurrentHP -= m_Atk;
    }

    // random move
    void RandomDirection()
    {
        m_Vector.Set(0, 0, m_Vector.z);
        m_RandomInt = Random.Range(0, 8);
        switch(m_RandomInt)
        {
            case 0:
                m_CurState = ENEMYSTATE.UP;
                break;
            case 1:
                m_CurState = ENEMYSTATE.DOWN;
                break;
            case 2:
                m_CurState = ENEMYSTATE.RIGHT;
                break;
            case 3:
                m_CurState = ENEMYSTATE.LEFT;
                break;
            default:
                m_CurState = ENEMYSTATE.IDLE;
                break;
        }
    }
}
