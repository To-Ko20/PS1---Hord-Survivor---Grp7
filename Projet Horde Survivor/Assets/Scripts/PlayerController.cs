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
    
    [SerializeField] private float MaxDashTime = 0.5f;
    [SerializeField] private float currentDashTime;
    [SerializeField] private float dashSpeed;
    
    private bool canDash = true;
    private bool isDashing;
    private Vector2 dashDirection;
    
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
                Dash();
            }
        }
    }
    
    void Dash()
    {
        Debug.Log("Dash");
        isDashing = true;
        currentDashTime = 0f;

        dashDirection = movement != Vector2.zero ? movement : Vector2.right;

        if (PlayerSkillHolderManager.Instance.hasDoubleDash && PlayerSkillHolderManager.Instance.dashNb == 2)
        {
            PlayerSkillHolderManager.Instance.dashNb--;
            StartCoroutine(WaitForDoubleDashCooldown());
        }
        else
        {
            canDash = false;
            StartCoroutine(WaitForDashCooldown());
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
        if (!canMove) return;

        if (isDashing)
        {
            rb.MovePosition(
                rb.position + dashDirection * dashSpeed * Time.fixedDeltaTime
            );

            currentDashTime += Time.fixedDeltaTime;

            if (currentDashTime >= MaxDashTime)
            {
                isDashing = false;
            }
        }
        else
        {
            rb.MovePosition(
                rb.position + movement * playerSpeed * Time.fixedDeltaTime
            );
        }
    }
}