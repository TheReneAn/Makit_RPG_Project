using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldQuest_Main : MonoBehaviour
{
    public GameObject m_Wall;

    private bool m_FieldQuestCheck;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(QuestManager.Instance.g_QuestList[2].m_State == Quest.QuestState.DONE && !m_FieldQuestCheck)
        {
            m_Wall.SetActive(false);
            m_FieldQuestCheck = true;
        }
    }
}
