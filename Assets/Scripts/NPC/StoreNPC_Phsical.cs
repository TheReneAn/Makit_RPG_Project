using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreNPC_Phsical : MonoBehaviour
{
    [Header("NPC Information")]
    // 1000: Genaral Store 
    // 2000: Armor Store 
    public int NPC_ID;
    public string NPC_Name;
    public Sprite NPC_Sprite;

    private bool Ontrigger = false;

    [Header("Dialoue Information")]
    public Dialogue thisdiag;

    public GameObject StoreNPC_SeletAction_Panel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NPC_Name = thisdiag.names[0];
            NPC_Sprite = thisdiag.sprites[0];

            Ontrigger = true;
            DialogueManager.instance.ShowDialogue(thisdiag);
        }
    }

    private void SetActive()
    {
        StoreNPC_SeletAction_Panel.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Ontrigger && !DialogueManager.instance.m_IsTalking)
        {
            SetActive();
        }
       
    }
}
