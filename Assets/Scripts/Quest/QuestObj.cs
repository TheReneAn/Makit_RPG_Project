using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestObj : MonoBehaviour
{
    public int m_QuestNum;           // quest index num
    public Dialogue m_NormalDiag;    // normal dialogue 
    public Dialogue m_StartDiag;     // Start dialogue
    public Dialogue m_CompleteDiag;  // complete dialogue
    public Dialogue m_EndDiag;       // complete dialogue
    public GameObject m_RewardPanel; // Reward panel

    public SpriteRenderer m_QuestRender; // quest marker sprite renderer
    public Sprite m_QuestAvailSprite;    // quest marker avilable sprite
    public Sprite m_QuestAcceptSprite;   // quest marker accept sprite
    public Sprite m_QuestCompleteSprite; // quest marker complete sprite

    public int m_CurTotalKill;   // kill quest check number
    public bool m_QuestNPC;

    private GameObject m_Player;    // player
    private bool m_CompleteQuest;   // check to complete quest
    private bool m_OnReward;   // check to complete quest

    // Start is called before the first frame update
    void Start()
    {
        m_CurTotalKill = GameManager.Instance.g_TotalKillCount;     // init killcount
        m_Player = GameObject.Find("Player");                       // find player
    }

    // Update is called once per frame
    void Update()
    {
        if(m_QuestNPC)
        {
            // Kill quest case
            if (QuestManager.Instance.g_QuestList[m_QuestNum].m_KillQuest == true)
            {
                // complete quest
                if (QuestManager.Instance.g_QuestList[m_QuestNum].m_QuestKillCount <= 1 && QuestManager.Instance.g_QuestList[m_QuestNum].m_State != Quest.QuestState.DONE)
                {
                    QuestManager.Instance.g_QuestList[m_QuestNum].m_State = Quest.QuestState.COMPLETE;
                }
                // if enemy died count decrease
                if (m_CurTotalKill != GameManager.Instance.g_TotalKillCount)
                {
                    QuestManager.Instance.g_QuestList[m_QuestNum].m_QuestKillCount--;
                    m_CurTotalKill = GameManager.Instance.g_TotalKillCount;
                }
            }
            // Move quest case
            else if (QuestManager.Instance.g_QuestList[m_QuestNum].m_MoveQuest == true)
            {
                // complete quest
                if (QuestManager.Instance.g_QuestList[m_QuestNum].m_GoalArrive == true && QuestManager.Instance.g_QuestList[m_QuestNum].m_State != Quest.QuestState.DONE)
                {
                    QuestManager.Instance.g_QuestList[m_QuestNum].m_State = Quest.QuestState.COMPLETE;
                }
            }
            else
            {

            }
        }
        
        // when complete the quest, making complete dialogue
        if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.COMPLETE)
        {
            if (!m_CompleteQuest)
                CompleteQuest();
            m_CompleteQuest = true;
        }

        // quest marker image
        if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.AVAILABLE)
        {
            m_QuestRender.sprite = m_QuestAvailSprite;
            m_QuestRender.color = Color.yellow;
        }
        else if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.ACCEPTED)
        {
            m_QuestRender.sprite = m_QuestAcceptSprite;
            m_QuestRender.color = Color.grey;
        }
        else if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.COMPLETE)
        {
            m_QuestRender.sprite = m_QuestCompleteSprite;
            m_QuestRender.color = Color.yellow;
        }
        else
        {
            m_QuestRender.sprite = null;
        }

        // reward process
        if (m_OnReward && !DialogueManager.instance.m_IsTalking && QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.COMPLETE)
        {
            RewardPanel();
            QuestManager.Instance.g_QuestList[m_QuestNum].m_State = Quest.QuestState.DONE;
        }
    }

    public void StartQuest()
    {
        // start quest dialogue
        DialogueManager.instance.ShowDialogue(m_StartDiag);
        // current quest adding
       QuestManager.Instance.g_QuestList[m_QuestNum].m_CurQuestNum = QuestManager.Instance.g_CurrentQuestList.Count;
       QuestManager.Instance.g_CurrentQuestList.Add(QuestManager.Instance.g_QuestList[m_QuestNum]);
    }
    public void CompleteQuest()
    {
        // start quest dialogue
        DialogueManager.instance.ShowDialogue(m_CompleteDiag);
    }
    public void EndQuest()
    {
        // quest end dialogue and reward
        DialogueManager.instance.ShowDialogue(m_EndDiag);


        GameManager.Instance.g_Money += QuestManager.Instance.g_QuestList[m_QuestNum].m_RewardMoney;
        GameManager.Instance.g_PlayerEXP += QuestManager.Instance.g_QuestList[m_QuestNum].m_RewardExp;
        // remove current quest
        QuestManager.Instance.g_CurrentQuestList.RemoveAt(QuestManager.Instance.g_QuestList[m_QuestNum].m_CurQuestNum);
    }

    public void NormalDiag()
    {
        // start quest dialogue
        DialogueManager.instance.ShowDialogue(m_NormalDiag);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // quest trigger collide
        if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.AVAILABLE)// && collision.gameObject.CompareTag("Player"))
        {
            QuestManager.Instance.g_QuestList[m_QuestNum].m_State = Quest.QuestState.ACCEPTED;
            StartQuest();
        }
        else if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.COMPLETE)
        {
            m_OnReward = true;
            EndQuest();
        }
        else if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.UNABLE || QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.DONE)
        {
            NormalDiag();
        }
    }
    public void CheckQuest()
    {
        // quest trigger collide
        if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.AVAILABLE)// && collision.gameObject.CompareTag("Player"))
        {
            QuestManager.Instance.g_QuestList[m_QuestNum].m_State = Quest.QuestState.ACCEPTED;
            StartQuest();
        }
        else if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.COMPLETE)
        {
            m_OnReward = true;
            EndQuest();
        }
        else if (QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.UNABLE || QuestManager.Instance.g_QuestList[m_QuestNum].m_State == Quest.QuestState.DONE)
        {
            NormalDiag();
        }
    }

    // rewand panel set active and reward data
    public void RewardPanel()
    {
        m_RewardPanel.SetActive(true);
        m_RewardPanel.GetComponent<QuestReward>().RewardText(QuestManager.Instance.g_QuestList[m_QuestNum].m_RewardExp, QuestManager.Instance.g_QuestList[m_QuestNum].m_RewardMoney);

    }

    // turn off the reward panel
    public void RewardPanelOff()
    {
        m_RewardPanel.SetActive(false);
    }
}
