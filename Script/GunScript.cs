using System.Collections.Generic;
using UnityEngine;

public class GunScript : Weapon
{
    public KeyCode[] kc;
    public GameObject bulletPrefab;
    public Transform Gun;
    public LayerMask whatIsGround;

    //public bool IsAiming = false;
    public bool IsOverWall = false;
    public bool ShootTrigger = false;
    //public bool IsSprinting = false;

    public FPS_Camera camera;

    public PlayerManager pm;

    void Update()
    {
        IsAiming = Input.GetKey(kc[1]);
        IsOverWall = CheckWall(whatIsGround, 1.7f) || IsSprinting;

        WeaponChange();

        if (pm.bulletCount[pm.bulletEquped] > 0 || pm.bulletEquped == "Default")
        {
            if (pm.bulletEquped == "Auto")
            {
                if (Input.GetKey(kc[0]) && !IsOverWall && pm.Reloads["Auto"] == 0f)
                {
                    ShootTrigger = true;
                    Shoot(pm.bulletEquped);
                }
            }
            else
            {
                if (Input.GetKeyDown(kc[0]) && !IsOverWall && pm.Reloads[pm.bulletEquped] == 0f)
                {
                    ShootTrigger = true;
                    Shoot(pm.bulletEquped);
                }
            }
        }

        Dictionary<string, float> ReloadTMP = new Dictionary<string, float>();
        foreach (KeyValuePair<string, float> key in pm.Reloads)
        {
            ReloadTMP.Add(key.Key, Mathf.Clamp(key.Value - Time.deltaTime, 0f, 1000f));
        }
        pm.Reloads = ReloadTMP;

        pm.inventory.text = pm.UpdateGun("Default");
        pm.inventory.text += pm.UpdateGun("Auto");
        pm.inventory.text += pm.UpdateGun("Shotgun");
    }

    public void WeaponChange()
    {
        if (Input.GetKey(kc[2]))
        {
            pm.bulletEquped = "Default";
        }
        if (Input.GetKey(kc[3]))
        {
            pm.bulletEquped = "Auto";
        }
        if (Input.GetKey(kc[4]))
        {
            pm.bulletEquped = "Shotgun";
        }
    }


    public bool CheckWall(LayerMask Walls, float ray)
    {
        return Physics.Raycast(transform.position, camera.gameObject.transform.forward, ray, Walls);
    }
    void Shoot(string name)
    {
        pm.bulletCount[name] = Mathf.Clamp(pm.bulletCount[name] - 1, 0, pm.bulletCount[name] + 1);
        float AimPenalty = 1f;
        if (IsAiming)
        {
            AimPenalty = 1.5f;
        }

        pm.Reloads[name] = pm.bulletDB[name][2] * AimPenalty;

        for (int i = 0; i < pm.bulletDB[name][4]; i++)
        {
            BulletRaycast(name, pm.bulletDB);
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

            try
            {
                var enemy = hit.collider.gameObject.transform.parent;

                if (enemy.tag == "enemy")
                {
                    enemy.GetComponentInParent<HealthManager>().TakingDamage(bullets[BulletName][0]);
                    bullet.GetComponent<BulletScript>().SummonBlood(hit.point);
                }
            }
            catch
            {
                bullet.GetComponent<BulletScript>().SummonDirt(hit.point, hit.normal, (hit.point - Gun.transform.position).normalized);
            }
        }
        else
        {
            bullet.GetComponent<BulletScript>().BulletInitialiaze(BulletName, Gun.position, accuracy * bullets[BulletName][1] + Gun.position);
        }
    }
}
