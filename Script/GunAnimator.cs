using Unity.VisualScripting;
using UnityEngine;

public class GunAnimator: MonoBehaviour
{
    public GunScript main;
    public ParticleSystem[] Particles;

    private Vector3 Target_direction = Vector3.zero;
    private Quaternion Target_rotation = Quaternion.identity;
    private FPS_Camera camera;

    private void Start()
    {
        camera = main.camera;
    }
    void Update()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, Target_direction, Time.deltaTime * 10f);// 0.05f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, Target_rotation, Time.deltaTime * 10f);// 0.05f) ;
        if (main.IsAiming)
        {
            camera.FOV = 50;
            Target_direction = new Vector3(0, -0.27f, 0.539f);
        }
        else
        {
            camera.FOV = 75;
            Target_direction = new Vector3(0.771f, -0.498f, 0.539f);
        }
        if (main.IsOverWall)
        {
            Target_rotation.eulerAngles = new Vector3 (-5, -90, 0);
        }
        else
        {
            Target_rotation.eulerAngles = Vector3.zero;
        }

        if (main.ShootTrigger)
        {
            main.ShootTrigger = false;
            if(main.bulletEquped == "auto")
            {
                LightShoot();
            }
            else
            {
                HeavyShoot();
            }
            camera.kickback += 10;
        }

    }
    



    void HeavyShoot()
    {
        Particles[0].Play();
    }
    void LightShoot()
    {
        Particles[1].Play();
    }
}
