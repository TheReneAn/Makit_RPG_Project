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
    public int m_Atk;
    public float m_AtkDelay;
    public GameObject m_Player;
    public float speed;

    private Vector2 m_PlayerPos;
    private int m_RandomInt;
    private ENEMYMOVING m_direction;
    private float m_TimeCount;
    protected Vector3 vector;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeCount += Time.deltaTime;

        if (m_TimeCount > 1f)
        {
            RandomDirection();
            m_TimeCount = 0;
        }

        switch (m_direction)
        {
            case ENEMYMOVING.UP:
                vector.y = 1f;
                break;
            case ENEMYMOVING.DOWN:
                vector.y = -1f;
                break;
            case ENEMYMOVING.LEFT:
                vector.x = -1f;
                break;
            case ENEMYMOVING.RIGHT:
                vector.x = 1f;
                break;
            default:
                vector.x = 0;
                vector.y = 0;
                break;
        }

        if (vector.x != 0)
        {
            transform.Translate(vector.x * speed, 0, 0);
        }
        else if (vector.y != 0)
        {
            transform.Translate(0, vector.y * speed, 0);
        }
    }

    private bool NearPlayer()
    {
        m_PlayerPos = m_Player.transform.position;

        if(Mathf.Abs(Mathf.Abs(m_PlayerPos.x) - Mathf.Abs(this.transform.position.x)) <= 2f)
        {
            if (Mathf.Abs(Mathf.Abs(m_PlayerPos.y) - Mathf.Abs(this.transform.position.y)) <= 2f)
            {
                return true;
            }
        }
        if (Mathf.Abs(Mathf.Abs(m_PlayerPos.y) - Mathf.Abs(this.transform.position.y)) <= 2f)
        {
            if (Mathf.Abs(Mathf.Abs(m_PlayerPos.x) - Mathf.Abs(this.transform.position.x)) <= 2f)
            {
                return true;
            }
        }

        return false;
    }

    private void RandomDirection()
    {
        vector.Set(0, 0, vector.z);
        m_RandomInt = Random.Range(0, 10);
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
