using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    [Header("Enemies")]
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    public Vector3 spawnArea;
    public float spawnTimer;

    private float timer;

    [Header("Score Stuff")]
    public int score;
    public int maxscore;
    public int enemy1score;
    public int enemy2score;
    public int enemy3score;

    [Header("DropStats")]
    public GameObject dropPrefab;
    public GameObject SmokePrefab;
    public float dropñChance;
    [SerializeField, Range(0f, 100f)] public float dropRandom;


    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0f)
        {
            if (score <= maxscore)
            {
                Spawn();
                timer = spawnTimer;
                spawnTimer -= 0.1f;
            }
            
        }
    }

    private void Spawn()
    {
        Vector3 position = new Vector3(UnityEngine.Random.Range(-spawnArea.x, spawnArea.x), spawnArea.y, UnityEngine.Random.Range(-spawnArea.z, spawnArea.z));
        
        switch (UnityEngine.Random.Range(0, 3))
        {
            case 0:
                GameObject newEnemy1 = Instantiate(enemy1);
                newEnemy1.transform.position = position;
                score += enemy1score;
                newEnemy1.GetComponent<HealthManager>().spawnManager = this;
                break;
            case 1:
                GameObject newEnemy2 = Instantiate(enemy2);
                newEnemy2.transform.position = position;
                score += enemy2score;
                newEnemy2.GetComponent<HealthManager>().spawnManager = this;
                break;
            case 2:
                GameObject newEnemy3 = Instantiate(enemy3);
                newEnemy3.transform.position = position;
                score += enemy3score;
                newEnemy3.GetComponent<HealthManager>().spawnManager = this;
                break;
        }
    }

    public void KillEntity(GameObject enemy)
    {
        if(enemy.tag == "Player")
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("Menu");
        }
        var smoke = Instantiate(SmokePrefab);
        smoke.GetComponent<ParticleSystem>().Play();
        smoke.transform.position = enemy.transform.position;

        if(enemy.name == enemy1.name + "(Clone)")
        {
            score -= enemy1score;
        }
        if (enemy.name == enemy2.name + "(Clone)") 
        {
            score -= enemy2score;
        }
        if(enemy.name == enemy3.name + "(Clone)")
        {
            score -= enemy3score;
        }

        Drop(enemy.transform.position);
        Destroy(enemy);
    }

    public void Drop(Vector3 position)
    {
        float bulletRandom = Random.Range(0f, 100f);


        if (bulletRandom >= dropñChance)
        {
            var drop = Instantiate(dropPrefab);
            drop.transform.position = position;

            bulletRandom = Random.Range(0f, 100f);

            if (bulletRandom >= dropRandom)
            {
                drop.GetComponent<DropScript>().CreateBullet("Auto", 10);
            }
            else
            {
                drop.GetComponent<DropScript>().CreateBullet("Shotgun", 1);
            }
        }
    }

}
