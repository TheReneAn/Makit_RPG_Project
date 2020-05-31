using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    #region Singleton
    private static EventManager instance = null;

    // Don't destroy
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // singleton
    public static EventManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion
    private GameObject m_player;            // player object
    private GameObject m_Dpad;            // player object
    private Vector2 m_PrevPlayerPos;    // start player position
    private float m_Dist;               // member distance 
    private float m_DistX;              // distance X 
    private float m_DistY;              // distance Y
    private bool m_PlayerMoveRight;     // is player move right?
    private bool m_PlayerMoveLeft;      // is player move left?
    private bool m_PlayerMoveUp;        // is player move up?
    private bool m_PlayerMoveDown;      // is player move down?
    private bool m_Checker;             // check the privous player position bool
    public bool g_IsEventEnd;             // check moving
    void Start()
    {
        m_player = GameObject.Find("Player");
        m_Checker = true;
        g_IsEventEnd = false;
    }

    void Update()
    {
        if(m_player == null)
        {
            m_player = GameObject.Find("Player");
        }
        g_IsEventEnd = false;
        // player moving process (when start event move) 
        if (m_PlayerMoveRight)
        {
            m_player.GetComponent<Player>().Animation();
            PlayerMoveRight();
        }
        else if (m_PlayerMoveLeft)
            PlayerMoveLeft();
        else if (m_PlayerMoveUp)
            PlayerMoveUp();
        else if (m_PlayerMoveDown)
        {
            m_player.GetComponent<Player>().Animation();
            PlayerMoveDown();
        }
    }

    // setting the first position
    private void CheckPlayerPosition()
    {
        m_PrevPlayerPos = m_player.transform.position; 
    }

    // move right
    public void PlayerMoveRight(float distX)
    {
        m_Dist = 0;
        m_PlayerMoveRight = true;
        m_DistX = distX;
    }
    private void PlayerMoveRight()
    {
        m_player.GetComponent<Player>().RightMove();
        if (m_Checker)
        {
            CheckPlayerPosition();
            m_Checker = false;
        }
        m_player.GetComponent<Player>().CanNotControl();

        if (m_Dist >= m_DistX)
        {
            m_PlayerMoveRight = false;
            g_IsEventEnd = true;
            m_Checker = true;
            m_player.GetComponent<Player>().CanControl();
        }
        m_Dist = Mathf.Abs(Mathf.Abs(m_player.transform.position.x) - Mathf.Abs(m_PrevPlayerPos.x));
    }

    // move left
    public void PlayerMoveLeft(float distX)
    {
        m_Dist = 0;
        m_PlayerMoveLeft = true;
        m_DistX = distX;
    }
    private void PlayerMoveLeft()
    {
        m_player.GetComponent<Player>().LeftMove();
        if (m_Checker)
        {
            CheckPlayerPosition();
            m_Checker = false;
        }
        m_player.GetComponent<Player>().CanNotControl();

        if (m_Dist >= m_DistX)
        {
            m_PlayerMoveLeft = false;
            g_IsEventEnd = true;
            m_Checker = true;
            m_player.GetComponent<Player>().CanControl();
        }
        m_Dist = Mathf.Abs(Mathf.Abs(m_player.transform.position.x) - Mathf.Abs(m_PrevPlayerPos.x));
    }

    // move up
    public void PlayerMoveUp(float distY)
    {
        m_Dist = 0;
        m_PlayerMoveUp = true;
        m_DistY = distY;
    }
    private void PlayerMoveUp()
    {
        m_player.GetComponent<Player>().TopMove();
        if (m_Checker)
        {
            CheckPlayerPosition();
            m_Checker = false;
        }
        m_player.GetComponent<Player>().CanNotControl();

        if (m_Dist >= m_DistY)
        {
            m_PlayerMoveUp = false;
            g_IsEventEnd = true;
            m_Checker = true;
            m_player.GetComponent<Player>().CanControl();
        }
        m_Dist = Mathf.Abs(Mathf.Abs(m_player.transform.position.y) - Mathf.Abs(m_PrevPlayerPos.y));
    }

    // move down
    public void PlayerMoveDown(float distY)
    {
        m_PlayerMoveDown = true;
        m_DistY = distY;
    }
    private void PlayerMoveDown()
    {
        m_player.GetComponent<Player>().DownMove();
        if (m_Checker)
        {
            m_Dist = 0;
            CheckPlayerPosition();
            m_Checker = false;
        }
        m_player.GetComponent<Player>().CanNotControl();

        if (m_Dist >= m_DistY)
        {
            m_PlayerMoveDown = false;
            g_IsEventEnd = true;
            m_Checker = true;
            m_player.GetComponent<Player>().CanControl();
        }
        m_Dist = Mathf.Abs(Mathf.Abs(m_player.transform.position.y) - Mathf.Abs(m_PrevPlayerPos.y));
    }

    public Vector2 PlayerVector()
    {
        return m_player.GetComponent<Player>().m_Vector;
    }
}
