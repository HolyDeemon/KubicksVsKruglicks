using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcanumScript : Weapon
{
    public Transform player;
    public float orb_range;
    private float timer;
    [SerializeField] private GameObject canvas;

    public Dictionary<string, List<float>> bulletDB = new Dictionary<string, List<float>>() // [0] - ���� �� ���������, [1] - ���������, [2] - ��, [3] - �������, [4]- ��� �� ����; 
    {
        {"Arcanum", new List<float>{5f, 1000f, 3f, 5f, 1f}},
    };

    private float Reload = 0f;

    public override void Attack()
    {
        if (Reload == 0)
        {
            BulletRaycast("Arcanum", bulletDB, gameObject.transform, true);
            Reload = bulletDB["Arcanum"][2];
        }
        base.Attack();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {

        timer += Time.deltaTime * (3f - Reload) * 10;
        if(timer >= Mathf.PI * 2)
        {
            timer = 0;
        }
        canvas.transform.LookAt(Camera.main.transform.forward + canvas.transform.position, Vector3.up);
        canvas.transform.position = new Vector3(Mathf.Sin(timer), 0, Mathf.Cos(timer)) * orb_range + transform.position;

        transform.LookAt(player.position + Vector3.up * 1f);

        Reload = Mathf.Clamp(Reload - Time.deltaTime, 0f, 1000f);

    }

}
