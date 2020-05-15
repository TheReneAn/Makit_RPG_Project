﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreNPC_Phsical : MonoBehaviour
{
    [Header("NPC Information")]
    // 1000: Genaral Store 
    // 1001: Armor Store 
    public int NPC_ID;
    public string NPC_Name;
    public Sprite NPC_Sprite;

    private bool Ontrigger = false;

    [Header("Dialoue Information")]
    public Dialogue thisdiag;

    [Header("Select Panel Information")]
    public GameObject StoreNPC_SelectAction_Panel;
    public StoreNPC_SeletAction newSelectAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            NPC_Name = thisdiag.names[0];
            NPC_Sprite = thisdiag.sprites[0];

            Ontrigger = true;
            DialogueManager.instance.ShowDialogue(thisdiag);

            // Setup
            newSelectAction.Setup(this);
        }
    }

    void Update()
    {
        // Finish talking
        if (Ontrigger && !DialogueManager.instance.m_IsTalking)
        {
            StoreNPC_SelectAction_Panel.SetActive(true);
            Ontrigger = false;
        }
    }

}