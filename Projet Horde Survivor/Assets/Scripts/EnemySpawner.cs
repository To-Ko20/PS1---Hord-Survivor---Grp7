using System.Collections.Generic;
using System.Collections;
using System.Net.Sockets;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private Transform target;
    
    [SerializeField] int currentWave = 0;
    [SerializeField] private float countdown;
    
    public Wave[] waves;
    
    [SerializeField] private Transform minSpawnPoint, maxSpawnPoint;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (currentWave >= waves.Length)
        {
            if (EnemyManager.Instance.activeEnemies.Count == 0)
            {
                GameManager.Instance.Win();
            }
            return;
        }
        
        countdown -= Time.deltaTime;

        if (countdown <= 0)
        {
            countdown = waves[currentWave].timeToNextWave;
            currentWave++;
            StartCoroutine(SpawnWave());
        }

        transform.position = target.position;
    }
    
    private IEnumerator SpawnWave()
    {
        if (currentWave < waves.Length)
        {
            for (int i = 0; i < waves[currentWave].enemies.Length; i++)
            {
                GameObject newEnemy = Instantiate(waves[currentWave].enemies[i], SpawnPoint(), transform.rotation);
                
                EnemyManager.Instance.RegisterEnemy(newEnemy);
                
                yield return new WaitForSeconds(waves[currentWave].timeToNextEnemy);
            }
        }
    }

    public Vector2 SpawnPoint()
    {
        float x = 0f;
        float y = 0f;

        int spawnSide = Random.Range(0, 4);

        switch (spawnSide)
        {
            //Côté haut
            case 0:
                x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);
                y = maxSpawnPoint.position.y;
                break;
            //Côté bas 
            case 1:
                x = Random.Range(minSpawnPoint.position.x, maxSpawnPoint.position.x);
                y = minSpawnPoint.position.y;
                break;
            //Côté droit
            case 2:
                x = maxSpawnPoint.position.x;
                y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);
                break;
            //Côté gauche
            case 3:
                x = minSpawnPoint.position.x;
                y = Random.Range(minSpawnPoint.position.y, maxSpawnPoint.position.y);
                break;
        }

        return new Vector2(x, y);
    }
}

[System.Serializable]
public class Wave
{
    public GameObject[] enemies;

    public float timeToNextEnemy;
    public float timeToNextWave;
}