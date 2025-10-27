using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public List<GameObject> activeEnemies = new List<GameObject>();

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
	}

	public void UnregisterEnemy(GameObject enemy)
	{
		activeEnemies.Remove(enemy);
	}
}