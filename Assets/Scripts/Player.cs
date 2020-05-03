using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_Speed;               // player speed
    public Animator m_Anim;             // player animator

    private Rigidbody2D m_Rigid;        // player rigidbody
    private float m_Horizontal;         // player horizontal moving
    private float m_Vertical;           // player vertical moving
    private bool m_IsHorizontalMove;    // player horizontal or not bool value

    private Vector3 vector;
    private BoxCollider2D boxCollider;
    public LayerMask layerMask;         // Non-passable layer settings.

    void Start()
    {
        m_Rigid = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        // check move
        m_Horizontal = Input.GetAxisRaw("Horizontal");
        m_Vertical = Input.GetAxisRaw("Vertical");

        // check button up and down
        bool hDown = Input.GetButtonDown("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vUp = Input.GetButtonUp("Vertical");

        // Check obstacles
        RaycastHit2D hit;   // hit = Null or obstacle
        Vector2 start = transform.position;      // The current location value of the character
        // The location value that the character wants to move to.
        Vector2 end = start + new Vector2(vector.x * m_Speed, vector.y * m_Speed);

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        if (hit.transform == null)
        {
            // Making dicision horizontal or not
            if (hDown)
            {
                m_IsHorizontalMove = true;
                m_Anim.SetBool("Idle", false);
            }
            else if (vDown)
            {
                m_IsHorizontalMove = false;
                m_Anim.SetBool("Idle", false);
            }
            else
            {
                m_Anim.SetBool("Idle", true);
            }

            // player idle check
            if (m_Vertical == 0 && m_Horizontal == 0)
            {
                m_Anim.SetBool("Idle", true);
            }
            else
            {
                m_Anim.SetBool("Idle", false);
            }

            // player moving animation
            if (m_Anim.GetInteger("HorizontalMove") != m_Horizontal)
            {
                m_Anim.SetBool("IsChange", true);
                m_Anim.SetInteger("HorizontalMove", (int)m_Horizontal);
            }
            else if (m_Anim.GetInteger("VerticalMove") != m_Vertical)
            {
                m_Anim.SetBool("IsChange", true);
                m_Anim.SetInteger("VerticalMove", (int)m_Vertical);
            }
            else
            {
                m_Anim.SetBool("IsChange", false);
            }
        }   
        
    }

    void FixedUpdate()
    {
        // player position move
        Vector2 moveVec = m_IsHorizontalMove ? new Vector2(m_Horizontal, 0) : new Vector2(0, m_Vertical);
        m_Rigid.velocity = moveVec * m_Speed;
    }
}
