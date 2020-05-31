using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScene : MonoBehaviour
{
    public string m_SceneName;          // move scene name
    private Player m_Player;            // player
    // Start is called before the first frame update
    void Start()
    {
        m_Player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when collide move to map
        if (collision.gameObject.name == "Player")
        {
            GameManager.Instance.g_CheckSceneChange = true;
            if (m_SceneName == "Village")
            {
                GameManager.Instance.g_CurrentSceneNum = 1;
            }
            else if(m_SceneName == "Field")
            {
                GameManager.Instance.g_CurrentSceneNum = 2;
            }
               
            GameManager.Instance.SceneChange(m_SceneName);
        }
    }
}
