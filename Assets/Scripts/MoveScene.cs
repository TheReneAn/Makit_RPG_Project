﻿using System.Collections;
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
            GameManager.Instance.SceneChange(m_SceneName);
        }
    }
}
