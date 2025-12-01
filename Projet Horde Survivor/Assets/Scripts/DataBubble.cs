using System.Collections;
using UnityEngine;

public class DataBubble : MonoBehaviour
{
    [SerializeField] private int clicksToGain;
    [SerializeField] private CircleCollider2D circleCollider2D;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float speed;

    void Update()
    {
        circleCollider2D.radius = PlayerSkillHolderManager.Instance.magnetRadius;
        speed = PlayerSkillHolderManager.Instance.magnetForce;
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (PlayerSkillHolderManager.Instance.hasMagnet)
        {
            if (collision.transform == PlayerController.Instance.transform)
            {
                Vector2 direction = (collision.transform.position - transform.position).normalized;
                rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, direction * speed, 18f * Time.deltaTime);
            } 
        }
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform == PlayerController.Instance.transform)
        {
            ClickerManager.Instance.clicks += clicksToGain;
            //ClickerManager.Instance.gainedData += clicksToGain;
            ClickerManager.Instance.DisplayUpdate();
            ClickerManager.Instance.PlayAnim();
            Destroy(gameObject);
        }
    }
}
