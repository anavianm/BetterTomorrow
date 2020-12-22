using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileMovement : MonoBehaviour
{
	
    public float velocityX = 0f;
    public float velocityY = 0f;
	public float speed = 5.0f;

	public float life = 10f;
	private float lifetime = 0;

	public float damage = 1.0f;
	public LayerMask enemies;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //if (transform.position.x > 100.0f || transform.position.x < -100.0f || transform.position.y > 100.0f || transform.position.y < -100.0f)
        //{
       //     Destroy(gameObject);
       // }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(velocityX*speed, velocityY*speed);

		lifetime += 0.1f;
		if (lifetime >= life){
			Destroy(gameObject);
		} 
    }


	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "EnemyFloat"){
			other.gameObject.GetComponent<EnemyFloat>().TakeDamage(damage);
		}
		else if (other.gameObject.tag == "EnemyGround"){
			other.gameObject.GetComponent<EnemyGround>().TakeDamage(damage);
		}
		else if (other.gameObject.tag == "EnemyLarge"){
		other.gameObject.GetComponent<FinalBossScript>().TakeDamage(damage);
		} 

		if (other.gameObject.tag != "Player") {Destroy(gameObject);}
	}
}
