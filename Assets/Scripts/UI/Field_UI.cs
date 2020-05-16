using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field_UI : MonoBehaviour
{
    public GameObject m_DiagMgrObj; // dialogue manager object call

    void Awake()
    {
        m_DiagMgrObj.SetActive(true); // dialogue manager object active
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
}
