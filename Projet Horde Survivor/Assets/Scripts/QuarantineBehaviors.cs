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
    
    public GameObject player;

    private float _t;
    private bool _isChasing;
    private bool _isLocked;
    private float rotation = 0f;

    private void FixedUpdate()
    {
        if (isActiveAndEnabled)
        {
            Countdown();  
        }
    }

    public void Activate()
    {
        Vector3 pos = player.transform.position;
        pos.x += 15;
        pos.y -= 15;
        transform.position = pos;
        foreach (GameObject zone in unactivatedZones)
        {
            zone.gameObject.SetActive(true);
        }
        timeToChase = Random.Range(rangeTimeToChase.x, rangeTimeToChase.y);
        timeEffect = Random.Range(rangeTimeEffect.x, rangeTimeEffect.y);
        
        _t = timeToChase;
        _isChasing = true;
        rb.constraints = RigidbodyConstraints2D.None;
    }
    
    private void Desactivate()
    {
        foreach (GameObject zone in activatedZones)
        {
            zone.gameObject.SetActive(false);
        }

        foreach (GameObject zone in unactivatedZones)
        {
            zone.gameObject.SetActive(false);
        }
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
            }
            FollowPlayer();
        }
        else if (_isLocked)
        {
            foreach (GameObject zone in activatedZones)
            {
                zone.SetActive(true);
            }

            foreach (GameObject zone in unactivatedZones)
            {
                zone.SetActive(false);
            }
            
            if (_t <= 0)
            {
                _isLocked = false;
            }
        }
        
        if (!(_isLocked || _isChasing))
        {
            Desactivate();
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
