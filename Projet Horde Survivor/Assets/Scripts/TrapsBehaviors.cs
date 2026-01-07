using System;
using UnityEngine;

public class TrapsBehaviors : MonoBehaviour
{
    [SerializeField] private float scanSpeed;
    [SerializeField] private Vector2 scanMovement;
    [SerializeField] private Rigidbody2D rb;

    private void FixedUpdate()
    {
        Quaternion quat;
        rb.MovePosition(
            rb.position + scanMovement * scanSpeed * Time.fixedDeltaTime
        );
        quat = Quaternion.LookRotation(Vector3.forward, scanMovement);
        transform.rotation = quat;
    }
}
