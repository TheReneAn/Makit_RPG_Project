using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagTest : MonoBehaviour
{
    [SerializeField]
    public Dialogue diag;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        // temp start of dialogue
        if(Input.GetKeyDown(KeyCode.T))
        {
            DialogueManager.instance.ShowDialogue(diag);
        }
    }
}
