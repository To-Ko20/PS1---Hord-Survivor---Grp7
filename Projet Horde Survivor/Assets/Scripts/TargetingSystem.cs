using UnityEngine;
using static UnityEngine.Vector3;

public class TargetingSystem : MonoBehaviour
{
	[SerializeField] private EnemySpawner enemySpawner;
	public Transform nearestEnemy;
	
	private Transform target;
	
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
	}

	void Update()
	{
		nearestEnemy = null;
		float nearestDistance = float.MaxValue;

		foreach (var enemy in enemySpawner.activeEnemies)
		{
			float distanceToTarget = Vector3.Distance(enemy.transform.position, target.position);

			if (distanceToTarget <= nearestDistance)
			{
				nearestDistance = distanceToTarget;
				nearestEnemy = enemy.transform;
			}
		}
		
		if (nearestEnemy != null)
		{
			Debug.DrawLine(nearestEnemy.position, target.position, Color.red);
		}
	}
}