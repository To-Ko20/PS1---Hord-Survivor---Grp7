using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
	public List<Transform> activeEnemies = new List<Transform>();

	public void RegisterEnemy(Transform enemy)
	{
		if (!activeEnemies.Contains(enemy))
			activeEnemies.Add(enemy);
	}

	public void UnregisterEnemy(Transform enemy)
	{
		if (activeEnemies.Contains(enemy))
			activeEnemies.Remove(enemy);
	}
}