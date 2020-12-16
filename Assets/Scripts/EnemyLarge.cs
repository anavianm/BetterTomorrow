using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLarge : MonoBehaviour
{
    public int health;

    public Transform playerTransform;

    private Rigidbody2D rb;

    public float gameBoundaryXMin = -30.0f;
    public float gameBoundaryXMax = 30.0f;
    public float gameBoundaryYMin = -30.0f;
    public float gameBoundaryYMax = 30.0f;

    private float timeBetweenJumps = 3.0f;
    private float timeSinceLastJump;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        health = 1;

        timeSinceLastJump = 0.0f;
    }

    // Update is called once per frame
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
            //donothingfornow
        }
    }

    void FixedUpdate()
    {
        if (timeSinceLastJump >= timeBetweenJumps)
        {
            Jump();
            timeSinceLastJump = 0.0f;
        }
    }

    void Jump()
    {
        int direction = 1;
        if (playerTransform.position.x < transform.position.x)
        {
            direction = -1;
        }

        rb.velocity = new Vector2(direction * 2.0f, 1.0f);
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
