using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public List<GameObject> activeEnemies = new List<GameObject>();
	public List<int> enemyColliderInstanceIDs  = new List<int>();
	public Material normalMaterial;
	public Material glitchMaterial;

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
		enemyColliderInstanceIDs.Add(enemy.GetComponent<Collider2D>().GetInstanceID());
	}

	public void UnregisterEnemy(GameObject enemy)
	{
		activeEnemies.Remove(enemy);
		enemyColliderInstanceIDs.Remove(enemy.GetComponent<Collider2D>().GetInstanceID());
	}
}