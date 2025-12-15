using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public float playerSpeed = 1f;
    
    public bool canMove = true;

    private Vector2 movement;
    
    [SerializeField] private const float MaxDashTime = 1f;
    [SerializeField] private float dashDistance = 10;
    [SerializeField] private float dashStoppingSpeed = 0.1f;
    [SerializeField] private float currentDashTime = 1f;
    [SerializeField] private float dashSpeed = 6;
    
    private bool canDash = true;
    
    public static PlayerController Instance;

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
    
    void Update()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (PlayerSkillHolderManager.Instance.hasDash && canDash)
        {
            if (Input.GetKeyDown("space"))
            {
                currentDashTime = 0;
                if (PlayerSkillHolderManager.Instance.dashNb == 2)
                {
                    PlayerSkillHolderManager.Instance.dashNb--;
                    StartCoroutine(WaitForDoubleDashCooldown());
                }
                else
                {
                    canDash = false;
                    PlayerSkillHolderManager.Instance.dashNb = 2;
                    StartCoroutine(WaitForDashCooldown()); 
                }
                
            }
        }
    }

    IEnumerator WaitForDashCooldown()
    {
        yield return new WaitForSeconds(2);
        canDash = true;
    }
    IEnumerator WaitForDoubleDashCooldown()
    {
        yield return new WaitForSeconds(2);
        PlayerSkillHolderManager.Instance.dashNb = 2;
    }

    void FixedUpdate()
    {
        float speed;
        speed = playerSpeed;
        
        if (canMove)
        {
            if(currentDashTime < MaxDashTime)
            {
                Debug.Log("calculate dash");
                movement *= dashDistance;
                currentDashTime += dashStoppingSpeed;
                speed = dashSpeed;
            }
            
            rb.MovePosition(rb.position + movement * (speed * Time.fixedDeltaTime));
        }
    }
}