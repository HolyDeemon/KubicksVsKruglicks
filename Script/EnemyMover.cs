using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyMover : Mover
{
    public Transform Player;
    public float AttackRange;
    public float MageFlyHeight;

    private void FixedUpdate()
    {
        MoveObject();
    }

    public virtual void DefaultPattern()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        orientation.LookAt(Player.transform.position);

        MoverInput(Vector3.forward, false, false);
    }

    public virtual void MagePattern()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        orientation.LookAt(Player.transform.position);
        
        if((Player.transform.position - transform.position).magnitude <= AttackRange)
        {
            MoverInput(Vector3.right, false, false);
        }
        else
        {
            MoverInput(Vector3.forward, false, false);
        }

        transform.position = new Vector3(transform.position.x, MageFlyHeight, transform.position.z);
    }
    
    public virtual void GruntPattern()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;

        orientation.LookAt(Player.transform.position);

        MoverInput(Vector3.forward, false, false);
    }
}
