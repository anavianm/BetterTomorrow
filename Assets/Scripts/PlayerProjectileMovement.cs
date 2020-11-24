using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileMovement : MonoBehaviour
{
    public float velocityX = 0.0f;
    public float velocityY = 0.0f;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.x > 20.0f || transform.position.x < -20.0f || transform.position.y > 20.0f || transform.position.y < -20.0f)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocityX, velocityY);
    }
}
