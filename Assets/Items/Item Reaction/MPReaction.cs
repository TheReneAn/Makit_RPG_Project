using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPReaction : MonoBehaviour
{
    public void Use(int amountToIncrease)
    {
        GameManager.Instance.g_PlayerCurrentMP += amountToIncrease;
        if (GameManager.Instance.g_PlayerCurrentMP >= GameManager.Instance.g_PlayerMP)
        {
            GameManager.Instance.g_PlayerCurrentMP = GameManager.Instance.g_PlayerMP;
        }
    }
}
