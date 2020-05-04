using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ENEMYMOVING
{
    UP,
    DOWN,
    LEFT,
    RIGHT,
    IDLE
}

public class Enemy : MonoBehaviour
{
    private Animator m_Anim;
    public int m_Atk;
    public float m_AtkDelay;
    public GameObject m_Player;
    public float m_Speed;

    private Vector2 m_PlayerPos;
    private int m_RandomInt;
    private ENEMYMOVING m_direction;
    private float m_TimeCount;
    private bool m_CanMove = true;
    protected Vector3 m_Vector;

    void Start()
    {
       m_Anim = GetComponent<Animator>();
    }

    void Update()
    {
        m_TimeCount += Time.deltaTime;
        m_PlayerPos = m_Player.transform.position;

        if (NearPlayer())
        {
            Atack();
        }
        else
        {
            RandomMove();
        }

        m_Anim.SetFloat("HorizonMove",m_Vector.x);
        m_Anim.SetFloat("VerticalMove", m_Vector.y);
        m_Anim.SetBool("IsWalking", true);


        if (m_Vector.x == 0 && m_Vector.y == 0)
        {
            m_Anim.SetBool("Idle", true);
        }
        else
        {
            m_Anim.SetBool("Idle", false);
        }
        EnemyMovingProcess();
    }

    void EnemyMovingProcess()
    {
        if (m_CanMove)
        {
            switch (m_direction)
            {
                case ENEMYMOVING.UP:
                    m_Vector.x = 0f;
                    m_Vector.y = 1f;
                    break;
                case ENEMYMOVING.DOWN:
                    m_Vector.x = 0f;
                    m_Vector.y = -1f;
                    break;
                case ENEMYMOVING.LEFT:
                    m_Vector.x = -1f;
                    m_Vector.y = 0f;
                    break;
                case ENEMYMOVING.RIGHT:
                    m_Vector.x = 1f;
                    m_Vector.y = 0f;
                    break;
                default:
                    m_Vector.x = 0;
                    m_Vector.y = 0;
                    break;
            }

            if (m_Vector.x != 0)
            {
                transform.Translate(m_Vector.x * m_Speed, 0, 0);
            }
            else if (m_Vector.y != 0)
            {
                transform.Translate(0, m_Vector.y * m_Speed, 0);
            }
            else
            {
            }
        }
    }

    bool NearPlayer()
    {

        if(Mathf.Abs(Mathf.Abs(m_PlayerPos.x) - Mathf.Abs(this.transform.position.x)) <= 0.5f)
        {
            if (Mathf.Abs(Mathf.Abs(m_PlayerPos.y) - Mathf.Abs(this.transform.position.y)) <= 0.5f)
            {
                m_CanMove = false;
                return true;
            }
        }
        if (Mathf.Abs(Mathf.Abs(m_PlayerPos.y) - Mathf.Abs(this.transform.position.y)) <= 0.5f)
        {
            if (Mathf.Abs(Mathf.Abs(m_PlayerPos.x) - Mathf.Abs(this.transform.position.x)) <= 0.5f)
            {
                m_CanMove = false;
                return true;
            }
        }

        m_CanMove = true;
        return false;
    }

    void RandomMove()
    {
        m_CanMove = true;
        if (m_TimeCount > 1f)
        {
            RandomDirection();
            Debug.Log(m_direction);
            m_TimeCount = 0;
        }
    }

    void Atack()
    {
        m_CanMove = false;
        float distanceX = 0;
        float distanceY = 0;

        distanceX = m_PlayerPos.x - transform.position.x;
        distanceY = m_PlayerPos.y - transform.position.y;

        if (Mathf.Abs(distanceX) > Mathf.Abs(distanceY))
        {
            if (distanceX > 0)
            {
                //m_Anim.SetTrigger("RightAtk");
                Debug.Log("RightAtk");
            }
            else
            {
                //m_Anim.SetTrigger("LeftAtk");
                Debug.Log("LeftAtk");
            }
        }
        else
        {
            if (distanceY > 0)
            {
                //m_Anim.SetTrigger("UpAtk");
                Debug.Log("UpAtk");
            }
            else
            {
                //m_Anim.SetTrigger("DownAtk");
                Debug.Log("DownAtk");
            }
        }
    }

    void RandomDirection()
    {
        m_Vector.Set(0, 0, m_Vector.z);
        m_RandomInt = Random.Range(0, 8);
        switch(m_RandomInt)
        {
            case 0:
                m_direction = ENEMYMOVING.UP;
                break;
            case 1:
                m_direction = ENEMYMOVING.DOWN;
                break;
            case 2:
                m_direction = ENEMYMOVING.RIGHT;
                break;
            case 3:
                m_direction = ENEMYMOVING.LEFT;
                break;
            default:
                m_direction = ENEMYMOVING.IDLE;
                break;
        }
    }
}

