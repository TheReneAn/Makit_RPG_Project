using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OkorCancel : MonoBehaviour
{
    private AudioManager theAudio;
    public string enter_sound;
    public string Okay_sound;
    public string cancel_sound;

    public Text Text_Iventory_Question;

    public bool activated;                      // Panel activated
    private bool result = true;                 // okay = true, cancel = false

    // Start is called before the first frame update
    void Start()
    {
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
        activated = false;
    }

    public void Click_Cancle()
    {
        theAudio.Play(cancel_sound);
        activated = false;
        result = false;
    }

    public void ShowQuestionText()
    {
        if (Inventory.instance.thisItemType == "Use")
        {
            Text_Iventory_Question.text = "Would you like to use the item?";
        }

        if (Inventory.instance.thisItemType == "Equip")
        {
            Text_Iventory_Question.text = "Would you like to wear the equipment?";
        }
    }

    public bool GetResult()
    {
        return result;  // ok = true, cancel = false
    }
}
