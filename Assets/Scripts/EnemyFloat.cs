using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    public int health;
    public int damageDealt;

    public Transform playerTransform;

    private float speedX = 0.05f;
    private float speedY = 0.01f;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int directionX = 1;
        int directionY = 1;
        if (playerTransform.position.x < transform.position.x)
        {
            directionX = -1;
        }
        if (playerTransform.position.y < transform.position.y)
        {
            directionY = -1;
        }

        rb.velocity = new Vector2(rb.velocity.x + (speedX * directionX), rb.velocity.y + (speedY * directionY));
    }
}
