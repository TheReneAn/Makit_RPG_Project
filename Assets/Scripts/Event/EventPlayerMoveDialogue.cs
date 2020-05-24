using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayerMoveDialogue : MonoBehaviour
{
    [Header("8-Up, 2-Down, 4-Left, 6-Right")]
    public int m_Direction;
    [Header("True-DiagFirst, False-AfterMove")]
    public bool m_DiagFirst;
    public bool m_EventOnce;
    public Dialogue m_Diag;
    public float m_MoveDistance;
    public GameObject m_SelfDestroy;

    private bool m_StartEvent;
    private bool m_MoveEnd;
    private bool m_DiagStart;
    private bool m_EventEnd;

    void Start()
    {
        m_StartEvent = false;
        m_MoveEnd = false;
        m_DiagStart = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (m_StartEvent)
        {
            if (m_DiagFirst)
            {
                if(!m_DiagStart)
                {
                    DiagEvent();
                    m_DiagStart = true;

                }
                if (!DialogueManager.instance.m_IsTalking && !m_MoveEnd)
                {
                    switch (m_Direction)
                    {
                        case 8:
                            MoveEventTop();
                            break;
                        case 2:
                            MoveEventDown();
                            break;
                        case 4:
                            MoveEventLeft();
                            break;
                        case 6:
                            MoveEventRight();
                            break;
                        default:
                            break;
                    }

                    m_MoveEnd = true;
                    m_EventEnd = true;
                }
            }
            else
            {
                if (!m_MoveEnd)
                {
                    switch (m_Direction)
                    {
                        case 8:
                            MoveEventTop();
                            break;
                        case 2:
                            MoveEventDown();
                            break;
                        case 4:
                            MoveEventLeft();
                            break;
                        case 6:
                            MoveEventRight();
                            break;
                        default:
                            break;
                    }
                    m_MoveEnd = true;
                }
                if (EventManager.Instance.g_IsEventEnd && !m_DiagStart)
                {
                    DiagEvent();
                    m_DiagStart = true;
                    m_EventEnd = true;
                }
            }
        }

        if (m_EventOnce && m_EventEnd)
        {
            Destroy(gameObject);
        }
    }

    void MoveEventRight()
    {
        EventManager.Instance.PlayerMoveRight(m_MoveDistance);
    }
    void MoveEventLeft()
    {
        EventManager.Instance.PlayerMoveLeft(m_MoveDistance);
    }
    void MoveEventTop()
    {
        EventManager.Instance.PlayerMoveUp(m_MoveDistance);
    }
    void MoveEventDown()
    {
        EventManager.Instance.PlayerMoveDown(m_MoveDistance);
    }

    void DiagEvent()
    {
        DialogueManager.instance.ShowDialogue(m_Diag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_StartEvent = true;
        m_MoveEnd = false;
        m_DiagStart = false;
    }
}
