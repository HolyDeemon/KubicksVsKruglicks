using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool IsSprinting;
    public bool IsAiming;
    public GameObject bulletPrefab;

    public virtual void Attack()
    {

    }

    public void BulletRaycast(string BulletName, Dictionary<string, List<float>> bullets, Transform Gun, bool hostile = false)
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
                if (hostile) 
                {
                    if (enemy.tag == "Player")
                    {
                        enemy.GetComponentInParent<HealthManager>().TakingDamage(bullets[BulletName][0]);
                        bullet.GetComponent<BulletScript>().SummonBlood(hit.point);
                    }
                }
                else
                {
                    if (enemy.tag == "enemy")
                    {
                        enemy.GetComponentInParent<HealthManager>().TakingDamage(bullets[BulletName][0]);
                        bullet.GetComponent<BulletScript>().SummonBlood(hit.point);
                    }
                }

            }
            catch
            {
                bullet.GetComponent<BulletScript>().SummonDirt(hit.point);
            }
        }
        else
        {
            bullet.GetComponent<BulletScript>().BulletInitialiaze(BulletName, Gun.position, accuracy * bullets[BulletName][1] + Gun.position);
        }
    }
}
