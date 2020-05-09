using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkorCancel : MonoBehaviour
{
    private Inventory thisInventory;

    private AudioManager theAudio;
    public string enter_sound;
    public string Okay_sound;
    public string cancel_sound;

    public Text Text_Iventory_Question;

    private bool result = true;                 // okay = true, cancel = false

    // Start is called before the first frame update
    void Start()
    {
        thisInventory = FindObjectOfType<Inventory>();

        theAudio = FindObjectOfType<AudioManager>();
        theAudio.Play(enter_sound);
    }

    // Update is called once per frame
    void Update()
    {
        ShowQuestionText();
    }

    public void Click_Ok()
    {
        theAudio.Play(Okay_sound);
        result = true;
        thisInventory.OkorCanclePanel.SetActive(false);
    }

    public void Click_Cancle()
    {
        theAudio.Play(cancel_sound);
        result = false;
        thisInventory.OkorCanclePanel.SetActive(false);
    }

    public void ShowQuestionText()
    {
        if (thisInventory.currentItem.itemType.ToString() == "Use")
        {
            Text_Iventory_Question.text = "Would you like to use the item?";
        }

        if (thisInventory.currentItem.itemType.ToString() == "Equip")
        {
            Text_Iventory_Question.text = "Would you like to wear the equipment?";
        }
    }

    public bool GetResult()
    {
        return result;  // ok = true, cancel = false
    }

}
