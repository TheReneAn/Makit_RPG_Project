using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Singleton
    private static EnemyManager instance = null;

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
    public static EnemyManager Instance
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

    public GameObject[] gameObj;
    public GameObject enemy;

    // Teddy part
    void Start()
    {
        // making enemy (prefab instantiate)
        for (int i = 0; i < gameObj.Length; i++)
        {
            Instantiate(enemy, gameObj[i].transform.position, Quaternion.identity);
        }
    }


    void Update()
    {
        
    }


    // Rene part
}
