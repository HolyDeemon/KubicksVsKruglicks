using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    private string name;
    private Vector3 target;
    private float damage;
    private float speed;
    private float LifeDelay;

    private float Gravity;
    void Update()
    {
        LifeDelay += Time.deltaTime;
        if(LifeDelay < 10f)
        {
            Gravity += 0.1f;
            transform.position += target * speed;
            transform.position += Vector3.down * Gravity;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetVars(string NAME, Vector3 TARGET, float DAMAGE, float SPEED)
    {
        name = NAME;
        target = TARGET;
        damage = DAMAGE;
        speed = SPEED;
    }
}
