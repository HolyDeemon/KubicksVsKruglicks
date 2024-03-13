using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public string EntityName;
    public float currentHp;
    public float maxHp;
    public bool isDead;

    public UIHealt hpbar;

    public SpawnManager spawnManager;

    private void Start()
    {
        currentHp = maxHp;
        isDead = false;
    }

    public void TakingDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0) { isDead = true; spawnManager.KillEntity(gameObject); }

        if(hpbar != null)
        {
            hpbar.ChangeHP(currentHp);
        }
        if (currentHp <= maxHp / 3)
        {
            hpbar.Warning();
        }
    }
}
