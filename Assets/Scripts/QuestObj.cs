using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObj : MonoBehaviour
{
    public int m_QuestNum;
    public string m_StartText;
    public string m_EndText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartQuest()
    {
        QuestManager.Instance.ShowQuestText(m_StartText);
    }

    public void EndQuest()
    {
        QuestManager.Instance.ShowQuestText(m_EndText);
        QuestManager.Instance.g_QuestCompleted[m_QuestNum] = true;
        gameObject.SetActive(false);
    }
}
