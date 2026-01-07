using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuarantineBehaviors : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Vector2 rangeTimeToChase;
    [SerializeField] private float timeToChase;
    [SerializeField] private Vector2 rangeTimeEffect;
    [SerializeField] private float timeEffect;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private List<GameObject> activatedZones;
    [SerializeField] private List<GameObject> unactivatedZones;
    
    [SerializeField] private GameObject player;

    private float _t;
    private bool _isChasing;
    private bool _isLocked;
    private float rotation = 0f;

    private void Start()
    {
        timeToChase = Random.Range(rangeTimeToChase.x, rangeTimeToChase.y);
        timeEffect = Random.Range(rangeTimeEffect.x, rangeTimeEffect.y);
        
        _t = timeToChase;
        _isChasing = true;
    }

    private void FixedUpdate()
    {
            Countdown();  
    }

    private void Countdown()
    {
        _t -= Time.fixedDeltaTime;

        if (_isChasing)
        {
            if (_t <= 0)
            {
                _isChasing = false;
                _isLocked = true;
                _t = timeEffect;
                rb.linearVelocity = Vector2.zero;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;

                foreach (GameObject zone in activatedZones)
                {
                    zone.SetActive(true);
                }

                foreach (GameObject zone in unactivatedZones)
                {
                    zone.SetActive(false);
                }
            }
            FollowPlayer();
        }
        else if (_isLocked)
        {
            if (_t <= 0)
            {
                _isLocked = false;
            }
        }
        
        if (!(_isLocked || _isChasing))
        {
            Destroy(this.gameObject);
        }
    }

    private void FollowPlayer()
    {
        if (transform.position == player.transform.position)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = ((Vector2)player.transform.position - rb.position).normalized;
            rb.linearVelocity = direction * speed; 
        }
        
        rotation += speed * 50 * Time.fixedDeltaTime;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }
}
