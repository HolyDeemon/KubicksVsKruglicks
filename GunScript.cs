using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;

public class GunScript : MonoBehaviour
{
    public FPS_Camera camera;
    public ParticleSystem[] Particles;
    public KeyCode[] kc;
    public LayerMask whatIsGround; 
    public bool shooted = false;
    public float reload = 1;


    private Vector3 Target_direction = Vector3.zero;
    private Quaternion Target_rotation = Quaternion.identity;
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Target_direction, 0.05f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Target_rotation, 0.05f);
        if (Input.GetKey(kc[1]))
        {
            camera.FOV = 50;
            Target_direction = new Vector3(0, -0.27f, 0.539f);
        }
        else
        {
            camera.FOV = 75;
            Target_direction = new Vector3(0.771f, -0.498f, 0.539f);
        }
        if (CheckWall(whatIsGround))
        {
            Target_rotation.eulerAngles = new Vector3 (-5, -90, 0);
            Debug.DrawRay(transform.position, camera.gameObject.transform.forward * 2f, Color.red);
        }
        else
        {
            Target_rotation.eulerAngles = Vector3.zero;
            Debug.DrawRay(transform.position, camera.gameObject.transform.forward * 2f, Color.blue); 
            
            if (Input.GetKeyDown(kc[0]))
            {
                shooted = true;
                HeavyShoot();
                Invoke("Reload", reload);
                camera.kickback += 10;
            }

        }
    }
    



    void HeavyShoot()
    {
        Particles[0].Play();
        shooted = false;
    }
    void LightShoot()
    {
        Particles[1].Play();
    }

    void Reload()
    {
        shooted = true;
    }

    public bool CheckWall(LayerMask Walls)
    {
        return Physics.Raycast(transform.position, camera.gameObject.transform.forward, 2.1f, Walls);
    }



}
