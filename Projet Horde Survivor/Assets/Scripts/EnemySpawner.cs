using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyToSpawn; //Ennemi à faire spawner
    
    public float timeToSpawn; //Délai attendu entre deux spawn
    private float spawnTimer; //Délai avant le prochain spawn
    
    [SerializeField] private Transform minSpawnPoint, maxSpawnPoint;

    private Transform target;
    
    void Start()
    {
        spawnTimer = timeToSpawn; //Initialisation du délai avant le prochain spawn
        
        target = GameObject.FindGameObjectWithTag("Player").transform; //détecte le joueur
    }

    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0)
        {
            spawnTimer = timeToSpawn; //Réinitialise le délai avant le prochain spawn
            
            Instantiate(enemyToSpawn, SpawnPoint(), transform.rotation); //fait spawner un ennemi
        }
        
        transform.position = target.position;
    }

    private Vector2 SpawnPoint() //Détermine la position du spawn d'un ennemi
    {
        float x = 0f;
        float y = 0f;

        int spawnSide = Random.Range(0, 4); //Choisit aléatoirement l'un des côté du rectangle de spawn

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