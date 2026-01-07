using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBehaviours : MonoBehaviour
{
    [SerializeField] private CircleCollider2D radius;
    [SerializeField] private Transform radiusDisplay;
    [SerializeField] private List<GameObject> inRadius;

    private bool _isWaitingToExplode;
    private bool _isAboutToExplode;

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
            if (!_isAboutToExplode)
            {
                inRadius.Add(other.gameObject);
                if (!_isWaitingToExplode)
                {
                    _isWaitingToExplode = true;
                    StartCoroutine(WaitForExplode());
                }
            }
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!_isAboutToExplode)
        {
            if (inRadius.Contains(other.gameObject))
            {
                inRadius.Remove(other.gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (EnemyManager.Instance.activeEnemies.Contains(other.gameObject))
        {
            Explode();
        }
    }

    IEnumerator WaitForExplode()
    {
        var spr = GetComponent<SpriteRenderer>();
        
        spr.enabled = false;
        yield return new WaitForSeconds(0.5f);
        spr.enabled = true;
        yield return new WaitForSeconds(0.5f);
        spr.enabled = false;
        
        yield return new WaitForSeconds(0.25f);
        spr.enabled = true;
        yield return new WaitForSeconds(0.25f);
        spr.enabled = false;
        yield return new WaitForSeconds(0.25f);
        spr.enabled = true;
        yield return new WaitForSeconds(0.25f);
        spr.enabled = false;
        
        yield return new WaitForSeconds(0.125f);
        spr.enabled = true;
        yield return new WaitForSeconds(0.125f);
        spr.enabled = false;
        yield return new WaitForSeconds(0.125f);
        spr.enabled = true;
        yield return new WaitForSeconds(0.125f);
        spr.enabled = false;
        yield return new WaitForSeconds(0.125f);
        spr.enabled = true;
        yield return new WaitForSeconds(0.125f);
        spr.enabled = false;
        yield return new WaitForSeconds(0.125f);
        spr.enabled = true;
        yield return new WaitForSeconds(0.125f);
        spr.enabled = false;
        _isWaitingToExplode = true;
        yield return new WaitForSeconds(0.06f);
        spr.enabled = true;
        Explode();
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void Explode()
    {
        foreach (var nmi in inRadius)
        {
            if (nmi != null)
            {
                nmi.GetComponent<EnemyMovement>().EnemyTakeDamage(PlayerSkillHolderManager.Instance.mineDMG, "mine");
            }
        }
        Destroy(gameObject);
    }
}
