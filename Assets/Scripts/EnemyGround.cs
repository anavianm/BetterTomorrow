using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyGround : MonoBehaviour
{
    private Rigidbody2D rb;

    public int health;
    public int damageDealt;

    public Transform playerTransform;

    private float speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        int direction = 1;
        if (playerTransform.position.x < transform.position.x)
        {
            direction = -1;
        }
        rb.velocity = new Vector2(rb.velocity.x + (speed * direction), rb.velocity.y);
    }
}
