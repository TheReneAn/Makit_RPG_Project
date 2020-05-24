using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventStart : MonoBehaviour
{
    public Dialogue diag;
    private bool m_End;
    // Start is called before the first frame update
    void Start()
    {
        MoveEvent();
        m_End = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (EventManager.Instance.g_IsEventEnd && !m_End)
            DiagEvent();
    }

    void MoveEvent()
    {
        EventManager.Instance.PlayerMoveRight(100.0f);
    }

    void DiagEvent()
    {
        DialogueManager.instance.ShowDialogue(diag);
        m_End = true;
    }
}
