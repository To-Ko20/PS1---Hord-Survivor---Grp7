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
		Debug.Log("TargetingSystem Update");
		nearestEnemy = null;
		float nearestDistance = shootRadius;

		foreach (var enemy in EnemyManager.Instance.activeEnemies)
		{
			Debug.Log("Find an enemy");
			float distanceToTarget = Distance(enemy.transform.position, target.position);
			Debug.Log(distanceToTarget);
			if (distanceToTarget <= nearestDistance)
			{
				Debug.Log("enemy nearby target");
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