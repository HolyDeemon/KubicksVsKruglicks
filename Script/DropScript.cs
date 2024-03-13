using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    public Transform BulletOrigin;
    public MeshRenderer Bullet;

    private float spiningTick;

    public string Name;
    public int count;
    public Material[] mats;

    // Update is called once per frame
    void Update()
    {
        spiningTick += Time.deltaTime * 2;

        BulletOrigin.transform.localPosition = new Vector3(0, Mathf.Cos(spiningTick)/5, 0);
        BulletOrigin.transform.Rotate(Vector3.up, Time.deltaTime * 30);
    }

    public void CreateBullet(string NAME, int COUNT)
    {
        Name = NAME;
        count = COUNT;
        if (NAME == "Auto")
        {
            Bullet.material = mats[0];
        }
        else
        {
            Bullet.material = mats[1];
        }

    }



}
