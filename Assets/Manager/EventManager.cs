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
    private Player m_player;            // player object
    private Vector2 m_PrevPlayerPos;    // start player position
    private float m_Dist;               // member distance 
    private float m_DistX;              // distance X 
    private float m_DistY;              // distance Y
    private bool m_PlayerMoveRight;     // is player move right?
    private bool m_PlayerMoveLeft;      // is player move left?
    private bool m_PlayerMoveUp;        // is player move up?
    private bool m_PlayerMoveDown;      // is player move down?
    private bool m_Checker;             // check the privous player position bool
    void Start()
    {
        m_player = FindObjectOfType<Player>();
        m_Checker = true;
    }

    void Update()
    {
        // player moving process (when start event move) 
        if (m_PlayerMoveRight)
            PlayerMoveRight();
        else if (m_PlayerMoveLeft)
            PlayerMoveLeft();
        else if (m_PlayerMoveUp)
            PlayerMoveUp();
        else if (m_PlayerMoveDown)
            PlayerMoveDown();
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
        m_player.RightMove();
        if (m_Checker)
        {
            CheckPlayerPosition();
            m_Checker = false;
        }
        m_player.CanNotControl();

        if (m_Dist >= m_DistX)
        {
            m_PlayerMoveRight = false;
            m_Checker = true;
            m_player.CanControl();
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
        m_player.LeftMove();
        if (m_Checker)
        {
            CheckPlayerPosition();
            m_Checker = false;
        }
        m_player.CanNotControl();

        if (m_Dist >= m_DistX)
        {
            m_PlayerMoveLeft = false;
            m_Checker = true;
            m_player.CanControl();
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
        m_player.TopMove();
        if (m_Checker)
        {
            CheckPlayerPosition();
            m_Checker = false;
        }
        m_player.CanNotControl();

        if (m_Dist >= m_DistY)
        {
            m_PlayerMoveUp = false;
            m_Checker = true;
            m_player.CanControl();
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
        m_player.DownMove();
        if (m_Checker)
        {
            m_Dist = 0;
            CheckPlayerPosition();
            m_Checker = false;
        }
        m_player.CanNotControl();

        if (m_Dist >= m_DistY)
        {
            m_PlayerMoveDown = false;
            m_Checker = true;
            m_player.CanControl();
        }
        m_Dist = Mathf.Abs(Mathf.Abs(m_player.transform.position.y) - Mathf.Abs(m_PrevPlayerPos.y));
    }
}
