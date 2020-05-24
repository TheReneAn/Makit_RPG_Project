using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMoveGoal : MonoBehaviour
{
    public int m_QuestNumGoal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && QuestManager.Instance.g_QuestList[m_QuestNumGoal].m_State == Quest.QuestState.ACCEPTED)
        {
            QuestManager.Instance.g_QuestList[m_QuestNumGoal].m_GoalArrive = true;
        }
    }
}
