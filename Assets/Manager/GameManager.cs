using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance = null;

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
    public static GameManager Instance
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

    // Teddy variable
    public int g_PlayerStr = 5;             // player status Str 
    public int g_PlayerDex = 5;             // player status dex
    public int g_PlayerCon = 5;             // player status con
    public int g_PlayerAtk = 0;             // player total attak 
    public int g_PlayerDef = 0;             // player total defence
    public int g_PlayerCrt = 0;             // player total critical
    public int g_PlayerAvd = 0;             // player total avoid
    public int g_PlayerHP = 100;            // player max hp
    public int g_PlayerMP = 100;            // player max np
    public int g_PlayerCurrentHP = 100;     // player current hp
    public int g_PlayerCurrentMP = 100;     // player current mp
    public int g_PlayerLevel = 1;           // player level
    public int g_PlayerEXP = 0;             // player exprience
    public int g_Money = 0;                 // player money
    public int g_CurrentSceneNum = 0;       // current scene number
    public bool g_CheckSceneChange;       // current scene number
    public int g_TotalKillCount = 0;        // total enemy kill count
    public int[] maxLevelUpEXP =           // level up exp
    {0, 100, 500, 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000};
    public Vector2 g_PlayerPosition;        // player position


    // Rene variable



    // Teddy Function




    // scene change function
    public void SceneChange(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Rene Function

}
