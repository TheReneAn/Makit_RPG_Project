using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestReward : MonoBehaviour
{
    public Text m_RewardText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RewardText(int exp, int money)
    {
        m_RewardText.text = "Exp - " + exp + "\n Money - " + money;
    }
}
