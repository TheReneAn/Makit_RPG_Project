using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillageQuest_Main : MonoBehaviour
{
    public GameObject m_LightQuest;
    public GameObject m_MainQuest;

    private bool m_CheckLight1Quest;
    private bool m_CheckLight2Quest;

    // Update is called once per frame
    void Update()
    {
        if(QuestManager.Instance.g_QuestList[1].m_State == Quest.QuestState.COMPLETE && !m_CheckLight1Quest)
        {
            QuestManager.Instance.g_QuestList[0].m_State = Quest.QuestState.AVAILABLE;
            m_CheckLight1Quest = true;
        }
        if (QuestManager.Instance.g_QuestList[2].m_State == Quest.QuestState.DONE && !m_CheckLight2Quest 
            && QuestManager.Instance.g_QuestList[1].m_State == Quest.QuestState.DONE)
        {
            m_LightQuest.SetActive(false);
            m_MainQuest.SetActive(true);
            m_CheckLight1Quest = true;
        }
    }
}
