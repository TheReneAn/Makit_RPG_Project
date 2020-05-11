using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReaction : MonoBehaviour
{
    public void Use(int amountToIncrease)
    {
        GameManager.Instance.g_PlayerCurrentHP += amountToIncrease;
        if (GameManager.Instance.g_PlayerCurrentHP >= GameManager.Instance.g_PlayerHP)
        {
            GameManager.Instance.g_PlayerCurrentHP = GameManager.Instance.g_PlayerHP;
        }
    }
}
