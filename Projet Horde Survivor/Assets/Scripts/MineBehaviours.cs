using System;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviours : MonoBehaviour
{
    [SerializeField] private CircleCollider2D radius;
    [SerializeField] private Transform radiusDisplay;
    [SerializeField] private List<GameObject> inRadius;

    void Start()
    {
        float radiusSize = PlayerSkillHolderManager.Instance.mineRadius;
        radius.radius = radiusSize/2;
        radiusDisplay.localScale = new Vector3(radiusSize, radiusSize, radiusSize);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (EnemyManager.Instance.activeEnemies.Contains(other.gameObject))
        {
            inRadius.Add(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (inRadius.Contains(other.gameObject))
        {
            inRadius.Remove(other.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (EnemyManager.Instance.activeEnemies.Contains(other.gameObject))
        {
            Explode();
        }
    }

    private void Explode()
    {
        foreach (GameObject nmi in inRadius)
        {
            if (nmi != null)
            {
                nmi.GetComponent<EnemyMovement>().EnemyTakeDamage(PlayerSkillHolderManager.Instance.mineDMG, "mine");
            }
        }
        Destroy(gameObject);
    }
}
