using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : Mover
{
    public Transform Player;
    public float AttackRange;
    public float MageFlyHeight;

    private void Update()
    {
        MoveToPlayer();
    }

    private void FixedUpdate()
    {
        MoveObject();
    }

    public void MoveToPlayer()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        Vector3 target = Player.position - transform.position;
        target.Normalize();

        orientation.rotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, target, Vector3.up), Vector3.up);

        Debug.DrawRay(transform.position, orientation.transform.forward, Color.blue);

        if(MageFlyHeight != 0)
        {
            transform.position = new Vector3(transform.position.x, MageFlyHeight, transform.position.z);
        }

        if ((Player.transform.position - transform.position).magnitude < AttackRange / 2)
        {
            MoverInput(Vector2.down, false, false);
        }
        else if ((Player.transform.position - transform.position).magnitude > AttackRange)
        {
            if((Player.transform.position - transform.position).magnitude > AttackRange * 2f)
            {
                MoverInput(Vector2.up, true, false);
            }
            else
            {
                MoverInput(Vector2.up, false, false);
            }
        }
        else
        {
            Vector3 vel = Vector3.zero;
            if(MageFlyHeight != 0)
            {
                vel = Vector3.right * (Random.Range(0, 1) - 0.5f) * 2;
            }
            MoverInput(vel, false, false);
            Attack();
        }
    }

    public virtual void Attack()
    {
        
    }

    

}
