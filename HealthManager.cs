using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public float currentHp;
    public float maxHp;
    public bool isDead;

    private void Start()
    {
        currentHp = maxHp;
        isDead = false;
    }

    public void TakingDamage(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0) { isDead = true; }
    }
}
