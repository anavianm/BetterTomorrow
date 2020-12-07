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

    private float speed = 1.0f;

    public float gameBoundaryXMin = -30.0f;
    public float gameBoundaryXMax = 30.0f;
    public float gameBoundaryYMin = -30.0f;
    public float gameBoundaryYMax = 30.0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        health = 1;
    }

    /* physics version
    void FixedUpdate()
    {
        int direction = 1;
        if (playerTransform.position.x < transform.position.x)
        {
            direction = -1;
        }
        rb.velocity = new Vector2(rb.velocity.x + (speed * direction), rb.velocity.y);
    }
    */

    void Update()
    {
        float xPosition = transform.position.x;
        float yPosition = transform.position.y;
        if (health <= 0)
        {
            PlayerData.enemiesKilled++;
            PlayerData.coins++;
            Destroy(gameObject);
        }
        else if (xPosition > gameBoundaryXMax || xPosition < gameBoundaryXMin || yPosition > gameBoundaryYMax || yPosition < gameBoundaryYMin)
        {
            Destroy(gameObject);
        }
        else
        {
            int direction = 1;
            if (playerTransform.position.x < transform.position.x)
            {
                direction = -1;
            }
            transform.position = new Vector2(transform.position.x + (speed * direction * Time.deltaTime), transform.position.y);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "PlayerProjectile")
        {
            health--;

            Destroy(collider.gameObject);
        }
    }
}
