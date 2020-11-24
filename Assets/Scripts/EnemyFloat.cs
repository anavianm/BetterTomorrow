using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFloat : MonoBehaviour
{
    public int health;

    public Transform playerTransform;

    public GameObject projectilePrefab;
    public Transform projectileParent;

    private float speedX = 1.0f;
    private float speedY = 0.7f;

    private Rigidbody2D rb;

    private double timeSinceLastShot;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();

        timeSinceLastShot = 0.0;

        health = 1;
    }

    /* physics version
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

    */

    void Update()
    {
        if (health <= 0)
        {
            PlayerData.enemiesKilled++;
            PlayerData.coins++;
            Destroy(gameObject);
        }
        else
        {
            float playerX = playerTransform.position.x;
            float playerY = playerTransform.position.y;
            float enemyX = transform.position.x;
            float enemyY = transform.position.y;

            float distanceToPlayer = Mathf.Sqrt(Mathf.Pow(playerX - enemyX, 2) + Mathf.Pow(playerY - enemyY, 2));

            //if the enemy is closer than 5 units and its been 3 seconds since last shot, shoot again
            if (distanceToPlayer < 8.0f && timeSinceLastShot > 3.0)
            {
                //calculate projectile velocity so it is a constant speed
                float speed = 5.0f;

                float y = enemyY - playerY;
                float x = enemyX - playerX;

                if (x == 0.0f)
                {
                    x += 0.001f;
                }
                if (y == 0.0f)
                {
                    y += 0.001f;
                }

                float ratio = x / y;

                float velocityY = Mathf.Abs(Mathf.Sqrt(speed / (ratio + 1.0f)));
                float velocityX = Mathf.Abs((velocityY * ratio));

                if (y > 0)
                {
                    velocityY = -velocityY;
                }
                if (x > 0)
                {
                    velocityX = -velocityX;
                }

                Debug.Log("Ratio: " + ratio + " X VEL: " + velocityX + " Y VEL: " + velocityY);

                ShootProjectile(enemyX, enemyY, velocityX, velocityY);
                timeSinceLastShot = 0.0f;
            }

            timeSinceLastShot += Time.deltaTime;

            //if the enemy is further than 4 units away from the player, then move
            if (distanceToPlayer > 4.0f || distanceToPlayer < 2.0f)
            {
                int directionX = 1;
                int directionY = 0;
                if (playerX < enemyX)
                {
                    directionX = -1;
                }
                if (playerY - enemyY < -2.0f)
                {
                    directionY = -1;
                }
                else if (playerY - enemyY > -1.5f)
                {
                    directionY = 1;
                }

                if (distanceToPlayer < 2.0f)
                {
                    directionX = -directionX;
                    directionY = -directionY;
                }

                float newPositionX = enemyX + (speedX * directionX * Time.deltaTime);
                float newPositionY = enemyY + (speedY * directionY * Time.deltaTime);

                transform.position = new Vector2(newPositionX, newPositionY);
            }
        }
    }

    void ShootProjectile(float x, float y, float velocityX, float velocityY)
    {
        GameObject spawnedProjectile = Instantiate(projectilePrefab, new Vector2(x, y), Quaternion.identity, projectileParent);
        spawnedProjectile.GetComponent<Projectile>().velocityX = velocityX;
        spawnedProjectile.GetComponent<Projectile>().velocityY = velocityY;
        spawnedProjectile.SetActive(true);
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
