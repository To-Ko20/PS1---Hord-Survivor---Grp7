using UnityEngine;
using static UnityEngine.Vector3;

public class TargetingSystem : MonoBehaviour
{
	[SerializeField] private EnemySpawner enemySpawner;
	[SerializeField] private float shootRadius;
	[SerializeField] private bool drawShootRadius;
	public Transform nearestEnemy;
	
	[SerializeField] private Transform target;
	
	void Start()
	{
		target = GameObject.FindGameObjectWithTag("Player").transform;
		enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawner>();
	}

	void Update()
	{
		nearestEnemy = null;
		float nearestDistance = shootRadius;

		foreach (var enemy in EnemyManager.Instance.activeEnemies)
		{
			float distanceToTarget = Distance(enemy.transform.position, target.position);
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

	private void OnDrawGizmos()
	{
		if (drawShootRadius)
		{
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(target.position, shootRadius);
		}
	}
}