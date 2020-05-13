using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillDamage : MonoBehaviour
{
    private float tempTime;
    private void Start()
    {
    }
    void Update()
    {
        tempTime += Time.deltaTime;
        if (tempTime > 0.1f)
        {
            GameManager.Instance.g_PlayerCurrentMP -= 30;
            gameObject.SetActive(false);
            tempTime = 0;
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>().IsHit();
        }
    }
}
