using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class Save : MonoBehaviour
{
    // player position check
    public GameObject m_Player;

    [System.Serializable] // secure
    public class Data // save data
    {
        public float playerX;
        public float playerY;
        public int playerLevel;
        public int playerEXP;
        public int playerHP;
        public int playerMP;
        public int playerCurrentHP;
        public int playerCurrentMP;
        public int playerMoney;
        public int playerSTR;
        public int playerDEX;
        public int playerCON;
        public int SceneNumber;
    }

    public Data m_Data;
    private string m_DataPath;  // save data path

    public void SaveGame()
    {
        // save data init
        GameManager.Instance.g_PlayerPosition = m_Player.transform.position;
        m_DataPath = Application.dataPath + "/SaveFile.dat";

        m_Data.playerX = GameManager.Instance.g_PlayerPosition.x;
        m_Data.playerY = GameManager.Instance.g_PlayerPosition.y;
        m_Data.playerLevel = GameManager.Instance.g_PlayerLevel;
        m_Data.playerEXP = GameManager.Instance.g_PlayerEXP;
        m_Data.playerHP = GameManager.Instance.g_PlayerHP;
        m_Data.playerMP = GameManager.Instance.g_PlayerMP;
        m_Data.playerCurrentHP = GameManager.Instance.g_PlayerCurrentHP;
        m_Data.playerCurrentMP = GameManager.Instance.g_PlayerCurrentMP;
        m_Data.playerMoney = GameManager.Instance.g_Money;
        m_Data.playerSTR = GameManager.Instance.g_PlayerStr;
        m_Data.playerDEX = GameManager.Instance.g_PlayerDex;
        m_Data.playerCON = GameManager.Instance.g_PlayerCon;
        m_Data.SceneNumber = GameManager.Instance.g_CurrentSceneNum;

        // save file binary making
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(m_DataPath);

        bf.Serialize(file, m_Data);
        file.Close();
    }
    public void LoadGame()
    {
        // Exist load File
        if(File.Exists(m_DataPath))
        {
            // file open
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(m_DataPath, FileMode.Open);
            m_Data = (Data)bf.Deserialize(file);
            file.Close();

            // Load data
            GameManager.Instance.g_PlayerPosition.x = m_Data.playerX;
            GameManager.Instance.g_PlayerPosition.y = m_Data.playerY;
            GameManager.Instance.g_PlayerLevel= m_Data.playerLevel;
            GameManager.Instance.g_PlayerEXP = m_Data.playerEXP;
            GameManager.Instance.g_PlayerHP = m_Data.playerHP;
            GameManager.Instance.g_PlayerMP =  m_Data.playerMP;
            GameManager.Instance.g_PlayerCurrentHP = m_Data.playerCurrentHP;
            GameManager.Instance.g_PlayerCurrentMP = m_Data.playerCurrentMP;
            GameManager.Instance.g_Money = m_Data.playerMoney;
            GameManager.Instance.g_PlayerStr = m_Data.playerSTR;
            GameManager.Instance.g_PlayerDex = m_Data.playerDEX;
            GameManager.Instance.g_PlayerCon = m_Data.playerCON;
            GameManager.Instance.g_CurrentSceneNum = m_Data.SceneNumber;

            // player position change
            m_Player.transform.position = new Vector2(GameManager.Instance.g_PlayerPosition.x, GameManager.Instance.g_PlayerPosition.y);
        }
    }
}
