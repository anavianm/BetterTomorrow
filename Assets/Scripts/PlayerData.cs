using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public GameObject playerProjectilePrefab;
    public Transform playerProjectileParent;


    public static int health;

    public static int coins;

    public static int enemiesKilled;

    private float shotCooldown = 1.0f;

    private float timeSinceLastShot;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        coins = 0;
        enemiesKilled = 0;
        timeSinceLastShot = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && timeSinceLastShot > shotCooldown)
        {
            ShootProjectile();
            timeSinceLastShot = 0.0f;
        }
        timeSinceLastShot += Time.deltaTime;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Projectile")
        {
            health += -10;
            Destroy(collider.gameObject);
        }
    }

    void ShootProjectile()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float mouseX = mousePos[0];
        float mouseY = mousePos[1];

        float playerX = transform.position[0];
        float playerY = transform.position[1];

        Debug.Log("MOUSE POS: " + Input.mousePosition + " player POS: " + transform.position);

        //calculate projectile velocity so it is a constant speed
        float speed = 7.5f;

        float y = playerY - mouseY;
        float x = playerX - mouseX;

        if (x == 0.0f)
        {
            x += 0.001f;
        }
        if (y == 0.0f)
        {
            y += 0.001f;
        }

        float ratio = Mathf.Abs(x / y);

        Debug.Log("X: " + x + " Y: " + y + " Ratio: " + ratio);

        float velocityY = Mathf.Sqrt(speed / (ratio + 1.0f));
        float velocityX = velocityY * ratio;

        if (y > 0)
        {
            velocityY = -velocityY;
        }
        if (x > 0)
        {
            velocityX = -velocityX;
        }

        Debug.Log("VELOCITYX: " + velocityX + " VelocityY: " + velocityY);

        GameObject spawnedProjectile = Instantiate(playerProjectilePrefab, new Vector2(playerX, playerY), Quaternion.identity, playerProjectileParent);
        spawnedProjectile.GetComponent<PlayerProjectileMovement>().velocityX = velocityX;
        spawnedProjectile.GetComponent<PlayerProjectileMovement>().velocityY = velocityY;
        //spawnedProjectile.GetComponent<PlayerProjectileMovement>().rb.velocity = new Vector2(velocityX, velocityY);
        spawnedProjectile.SetActive(true);
    }

}
