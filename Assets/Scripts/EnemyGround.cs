using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class EnemyGround : MonoBehaviour
{
	private GameHandler gameHandler;
    private Rigidbody2D rb;
    public int health;
    public int damageDealt;

    public Transform playerTransform;
	public Transform ScreenBottom;
	//private Transform spawnPos;
	//public float bottomLimit = 100.0f;

    private float speed = 1.0f;

    //public float gameBoundaryXMin = -3000.0f;
    //public float gameBoundaryXMax = 3000.0f;
    //public float gameBoundaryYMin = -3000.0f;
    //public float gameBoundaryYMax = 3000.0f;

	public LayerMask enemies;
	public float damage = 1;


	private float DifficultyTimer;
	public float NextDifficulty = 10.0f;
	public int DamageIncrease = 1;


	void Awake(){

		//spawnPos = gameObject.transform;

	}

    // Start is called before the first frame update
    void Start()
    {
		playerTransform = GameObject.FindWithTag("Player").transform;
		ScreenBottom = GameObject.FindWithTag("screenBottom").transform;
		gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
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
        //else if (xPosition > gameBoundaryXMax || xPosition < gameBoundaryXMin || yPosition > gameBoundaryYMax || yPosition < gameBoundaryYMin)
		else if (yPosition <= ScreenBottom.position.y)
        {
            Destroy(gameObject);
			Debug.Log("Groundling died from a fall");
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

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer != enemies){
			if (other.gameObject.tag == "Player"){
				gameHandler.TakeDamage(damage);
			}
		}
	}


	public void TakeDamage(int damage){
		health -= damage;
	}


	void FixedUpdate(){
		DifficultyTimer +=0.01f;
		if (DifficultyTimer >= NextDifficulty){
			damage += DamageIncrease;
			DifficultyTimer = 0f;
		}
	}


}
