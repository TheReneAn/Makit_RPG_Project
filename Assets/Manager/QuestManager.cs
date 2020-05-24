using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    #region Singleton
    private static QuestManager instance = null;

    // Don't destroy
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // singleton
    public static QuestManager Instance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    #endregion

    public List<Quest> g_QuestList = new List<Quest>();         // all quest list
    public List<Quest> g_CurrentQuestList = new List<Quest>();  // current quest list
}
