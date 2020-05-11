using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Singleton
    private static CameraManager instance = null;

    // Don't destroy
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // singleton
    public static CameraManager Instance
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
    #endregion]

    private GameObject m_Target;   // tracking target
    private float m_MoveSpeed;     // camera speed
    public BoxCollider2D m_Bound; // boundary

    private Vector3 m_TargetPosition; // position
    private Vector3 m_MinBound;       // min boundary
    private Vector3 m_MaxBound;       // max boundary

    private float m_HalfWidth;        // CenterX
    private float m_HalfHeight;       // CenterY

    private Camera m_Camera;          // camera obj

    void Start()
    {
        // init
        m_Camera = FindObjectOfType<Camera>();
        m_Target = FindObjectOfType<Player>().gameObject;
        m_MoveSpeed = 3.0f;
        m_MinBound = m_Bound.bounds.min;
        m_MaxBound = m_Bound.bounds.max;
        m_HalfHeight = m_Camera.orthographicSize;
        m_HalfWidth = m_HalfHeight * Screen.width / Screen.height;
    }

    void Update()
    {
        if(m_Target.gameObject != null)
        {
            // camera movement
            m_TargetPosition.Set(m_Target.transform.position.x, m_Target.transform.position.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, m_TargetPosition, m_MoveSpeed * Time.deltaTime);

            float clampedX = Mathf.Clamp(transform.position.x, m_MinBound.x + m_HalfWidth, m_MaxBound.x - m_HalfWidth);
            float clampedY = Mathf.Clamp(transform.position.y, m_MinBound.y + m_HalfHeight, m_MaxBound.y - m_HalfHeight);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }

    // set new boundary setting func
    public void SetBound(BoxCollider2D newBound)
    {
        m_Bound = newBound;
        m_MinBound = m_Bound.bounds.min;
        m_MaxBound = m_Bound.bounds.max;
    }
}
