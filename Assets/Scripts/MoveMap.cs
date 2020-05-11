using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveMap : MonoBehaviour
{
    public Transform m_Target;            // move to target
    public BoxCollider2D m_TargetBound;   // target boundary

    private Player m_Player;            // player
    private CameraManager m_Camera;     // camera
    // Start is called before the first frame update
    void Start()
    {
        m_Camera = FindObjectOfType<CameraManager>();
        m_Player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when collide move to map
        if(collision.gameObject.name == "Player")
        {
            m_Camera.transform.position = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, m_Camera.transform.position.z);
            m_Camera.SetBound(m_TargetBound);
            m_Player.transform.position = new Vector3(m_Target.transform.position.x, m_Target.transform.position.y, 0);
        }
    }
}
