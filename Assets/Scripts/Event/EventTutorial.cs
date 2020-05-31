using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTutorial : MonoBehaviour
{
    public Dialogue m_Diag;
    public Dialogue m_Diag2;
    public GameObject m_Particle;

    private bool m_StartEvent;
    private bool m_Start2Event;
    private bool m_Start3Event;

    public Transform m_Target;            // move to target
    public BoxCollider2D m_TargetBound;   // target boundary
    public float m_CameraSize;

    private Player m_Player;            // player
    private CameraManager m_Camera;     // camera
    
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = FindObjectOfType<CameraManager>();
        m_Player = FindObjectOfType<Player>();
        Instantiate(m_Particle, gameObject.transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (!DialogueManager.instance.m_IsTalking && m_StartEvent)
        {
            MoveMap();
            m_StartEvent = false;
            m_Start2Event = true;
            EventManager.Instance.PlayerMoveUp(1);
        }
        if (EventManager.Instance.g_IsEventEnd && m_Start2Event)
        {
            m_Start2Event = false;
            m_Start3Event = true;
            DialogueManager.instance.ShowDialogue(m_Diag2);
        }
        if (!DialogueManager.instance.m_IsTalking && m_Start3Event)
        {
            GameManager.Instance.g_TutorialEnd = false;
            GameManager.Instance.g_CurrentSceneNum = 1;
            GameManager.Instance.SceneChange("Village");
        }
    }

    void MoveEvent()
    {
        EventManager.Instance.PlayerMoveRight(100.0f);
    }

    void DiagEvent()
    {
        DialogueManager.instance.ShowDialogue(m_Diag);
        m_StartEvent = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DiagEvent();
    }

    void MoveMap()
    {
        m_Camera.transform.position = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, m_Camera.transform.position.z);
        m_Camera.SetBound(m_TargetBound);
        m_Camera.SetCameraSize(m_CameraSize);
        m_Player.transform.position = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, 0);
    }
}
