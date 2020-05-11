using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
enum ENEMYSPECIES // Enemy state enum
{
    GOBLIN,
    SLIME,
    WOLF
}

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    //private static EnemyManager instance = null;

    //// Don't destroy
    //private void Awake()
    //{
    //    if (null == instance)
    //    {
    //        instance = this;
    //        DontDestroyOnLoad(gameObject);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    //// singleton
    //public static EnemyManager Instance
    //{
    //    get
    //    {
    //        if (null == instance)
    //        {
    //            return null;
    //        }
    //        return instance;
    //    }
    //}
    #endregion

    public GameObject[] m_GameObj;
    public GameObject m_GameBossObj;
    public GameObject m_Enemy;
    public GameObject m_Boss;
    // Teddy part

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SpawnEnemy();
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {

    }

    void Update()
    {
    }

    public void SpawnEnemy()
    {
        // making enemy (prefab instantiate)
        for (int i = 0; i < m_GameObj.Length; i++)
        {
            Instantiate(m_Enemy, m_GameObj[i].transform.position, Quaternion.identity);
        }
        // making boss (prefab instantiate)
        Instantiate(m_Boss, m_GameBossObj.transform.position, Quaternion.identity);
    }
}
