using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeScript : Weapon
{
    public GameObject melee;
    public GameObject shooter;
    public bool IsSlashed = true;
    public string Name;
    public Dictionary<string, List<float>> weaponDB = new Dictionary<string, List<float>>
    {
        {"sickle", new List<float>{5f, 3f, 1f, 0f, 0f}},
        {"club", new List<float>{10f, 5f, 1f, 0f, 0f}},
    };

    private float Reload;

    private Transform player;

    public override void Attack()
    {

        if (Reload == 0)
        {
            IsSlashed = false;
            Reload = weaponDB[Name][1];
            BulletRaycast(Name, weaponDB, shooter.transform, true);
        }
        base.Attack();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Reload <= weaponDB[Name][1] * 0.8f)
        {
            IsSlashed = true;
        }

        transform.LookAt(player.position, Vector3.up);

        if (!IsSlashed)
        {
            if(Name == "sickle")
            {
                melee.transform.localRotation = Quaternion.Lerp(melee.transform.localRotation, Quaternion.Euler(new Vector3(0, -90, 90)), Time.deltaTime * 10);
            }
            else
            {
                melee.transform.localRotation = Quaternion.Lerp(melee.transform.localRotation, Quaternion.Euler(new Vector3(-75, -95, 0)), Time.deltaTime * 10);
            }
        }
        else
        {
            melee.transform.localRotation = Quaternion.Lerp(melee.transform.localRotation, Quaternion.Euler(new Vector3(0, 0, 0)), Time.deltaTime * 2);
        }

        Reload = Mathf.Clamp(Reload - Time.deltaTime, 0f, 1000f);

    }

}
