using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public List<GameObject> activeEnemies = new List<GameObject>();
	public List<int> enemyColliderInstanceIDs  = new List<int>();

	public static EnemyManager Instance;
	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}
	
	public void RegisterEnemy(GameObject enemy)
	{
		activeEnemies.Add(enemy);
		enemyColliderInstanceIDs.Add(GetComponent<Collider2D>().GetInstanceID());
	}

	public void UnregisterEnemy(GameObject enemy)
	{
		activeEnemies.Remove(enemy);
		enemyColliderInstanceIDs.Remove(GetComponent<Collider2D>().GetInstanceID());
	}
}