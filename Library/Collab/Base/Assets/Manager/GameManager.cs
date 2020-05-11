﻿using System.Collections;
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
    public int g_PlayerStr = 5;
    public int g_PlayerDex = 5;
    public int g_PlayerCon = 5;
    public int g_PlayerAtk = 0;
    public int g_PlayerDef = 0;
    public int g_PlayerAvd = 0;
    public int g_PlayerHP = 100;
    public int g_PlayerMP = 100;
    public int g_PlayerCurrentHP = 100;
    public int g_PlayerCurrentMP = 100;
    public int g_PlayerLevel = 1;
    public int g_PlayerEXP = 0;
    public int g_Money = 0;
    public int g_CurrentSceneNum = 0;
    public Vector2 g_PlayerPosition;


    // Rene variable



    // Teddy Function




    // scene change function
    public void SceneChange(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    // Rene Function

}
