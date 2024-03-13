using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BulletScript : MonoBehaviour
{
    private string named;
    private float lifetime = 0.5f;
    private LineRenderer lineRenderer;

    public ParticleSystem[] particle;
    public GameObject bloodPrefab;
    private ParticleSystem explode;
    private float initialSize;
    private float initiallifetime;
    private Vector3 end;

    private void Update()
    {
        lifetime = Mathf.Clamp(lifetime - Time.deltaTime, 0, initiallifetime);
        lineRenderer.widthMultiplier = initialSize * (lifetime / initiallifetime);
        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void BulletInitialiaze(string NAME, Vector3 START, Vector3 TARGET)
    {
        lineRenderer = GetComponent<LineRenderer>();

        initialSize = lineRenderer.widthMultiplier;
        initiallifetime = lifetime;
        named = NAME;
        lineRenderer.SetPosition(0, START);
        lineRenderer.SetPosition(1, TARGET);

        if (name != "shot" || name != "explode")
        {
            SummonPiercedAir(START, TARGET);
        }
    }
    public void SummonDirt(Vector3 position, Vector3 normal, Vector3 normalized)
    {
        normal += position;
        //Vector3 targetPosition = Vector3.Project(position, normal).magnitude * normal * 2 + position + normalized;
        var Dirt = particle[0];
        Dirt.gameObject.transform.position = position;
        //Dirt.gameObject.transform.LookAt(targetPosition);
        Dirt.Play();
    }

    public void SummonPiercedAir(Vector3 start, Vector3 end)
    {
        for(int i = 5; i < (end - start).magnitude; i+=5) 
        {
            var air = Instantiate(particle[1].gameObject, gameObject.transform);
            air.transform.position = start + (end - start).normalized * i;
            air.GetComponent<ParticleSystem>().Play();
        }
    }

    public void SummonBlood(Vector3 position)
    {
        var blood = Instantiate(bloodPrefab, gameObject.transform);
        blood.transform.position = position;
        blood.GetComponent<ParticleSystem>().Play();
    }

}
