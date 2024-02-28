using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public KeyCode[] kc;
    public FPS_Camera camera;
    public GameObject bulletPrefab;
    public Transform Gun;
    public LayerMask whatIsGround;

    public string bulletEquped = "default";

    public bool IsAiming = false;
    public bool IsOverWall = false;
    public bool ShootTrigger = false;
    public bool IsSprinting = false;
    

    private Dictionary<string, List<float>> bulletDB = new Dictionary<string, List<float>>() // [0] - урон за попадание, [1] - дистанция, [2] - кд, [3] - разброс, [4]- кол во пуль; 
    {
        {"default", new List<float>{5f, 1000f, 1f, 0f, 1f}}, 
        {"auto", new List<float>{2f, 20f, 0.2f, 5f, 1f}}, 
        {"shot", new List<float>{1f, 5f, 2f, 45f, 10f}}, 
        {"explode", new List<float>{10f, 20f, 4f, 0f, 1f}}
    };
    public Dictionary<string, float> Reloads = new Dictionary<string, float>()
    {
        {"default", 0f},
        {"auto", 0f},
        {"shot", 0f},
        {"explode", 0f}
    };

    void Update()
    {
        WeaponSwapCheck();

        IsAiming = Input.GetKey(kc[1]);
        IsOverWall = CheckWall(whatIsGround, 1.7f) || IsSprinting;

        if(bulletEquped == "auto")
        {
            if (Input.GetKey(kc[0]) && !IsOverWall && Reloads["auto"] == 0f)
            {
                ShootTrigger = true;
                Shoot(bulletEquped);
            }
        }
        else
        {
            if (Input.GetKeyDown(kc[0]) && !IsOverWall && Reloads[bulletEquped] == 0f)
            {
                ShootTrigger = true;
                Shoot(bulletEquped);
            }
        }

        Dictionary<string, float> ReloadTMP = new Dictionary<string, float>();
        foreach (KeyValuePair<string, float> key in Reloads)
        {
            ReloadTMP.Add(key.Key, Mathf.Clamp(key.Value - Time.deltaTime, 0f, 1000f));
        }
        Reloads = ReloadTMP;
    }
    void WeaponSwapCheck()
    {
        if (Input.GetKeyDown(kc[2]))
        {
            bulletEquped = "default";
        }
        if (Input.GetKeyDown(kc[3]))
        {
            bulletEquped = "auto";
        }
        if (Input.GetKeyDown(kc[4]))
        {
            bulletEquped = "shot";
        }
        /*if (Input.GetKeyDown(kc[5]))
        {
            bulletEquped = "explode";
        }*/
    }
    public bool CheckWall(LayerMask Walls, float ray)
    {
        return Physics.Raycast(transform.position, camera.gameObject.transform.forward, ray, Walls);
    }
    void Shoot(string name)
    {
        float AimPenalty = 1f;
        if (IsAiming)
        {
            AimPenalty = 1.5f;
        }

        Reloads[name] = bulletDB[name][2] * AimPenalty;

        for (int i = 0; i < bulletDB[name][4]; i++)
        {
            BulletRaycast(name, bulletDB);
        }
    }
    void BulletRaycast(string BulletName, Dictionary<string, List<float>> bullets)
    {
        Vector3 accuracy = Quaternion.Euler(Random.Range(0, bullets[BulletName][3] * 2) - bullets[BulletName][3], Random.Range(0, bullets[BulletName][3] * 2) - bullets[BulletName][3], 0) * transform.forward;
        var bullet = Instantiate(bulletPrefab);

        RaycastHit hit;

        if (Physics.Raycast(Gun.position, accuracy, out hit, bullets[BulletName][1]))
        {
            bullet.GetComponent<BulletScript>().BulletInitialiaze(BulletName, Gun.transform.position, hit.point);
            bullet.GetComponent<BulletScript>().SummonDirt(hit.point, hit.normal);
            return;
        }
        else
        {
            bullet.GetComponent<BulletScript>().BulletInitialiaze(BulletName, Gun.position, accuracy * bullets[BulletName][1] + Gun.position);
        }


        
    }
}
