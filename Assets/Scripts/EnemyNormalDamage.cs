using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNormalDamage : MonoBehaviour
{
    public int m_Atk;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.g_PlayerCurrentHP -= m_Atk;
        }
    }
}
