using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    public enum QuestState { UNABLE, AVAILABLE, ACCEPTED, COMPLETE, DONE }  // Quest states

    public QuestState m_State;      // states variable
    public string m_QuestName;      // quest name
    public int m_Index;             // quest number
    public string m_Description;    // quest discription

    public int m_CurQuestNum;       // cur quest number
    public bool m_KillQuest;        // kill quest
    public int m_QuestKillCount;    // kill quest count
    public bool m_MoveQuest;        // move quest
    public bool m_GoalArrive;       // check move point arrive
    public BoxCollider2D m_Goal;    // move point collide

    public int m_RewardMoney;       // reward money
    public int m_RewardExp;         // reward Exp
}
