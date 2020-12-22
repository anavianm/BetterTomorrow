using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private GameHandler gameHandler;
    public float velocityX = 0.0f;
    public float velocityY = 0.0f;
	public float speed = 2.0f;

	public float life = 20f;
	private float lifetime = 0;

	public LayerMask ignoreThese;
	public float damage = 1;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start(){
		gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

   void Update(){
//        if (transform.position.x > 20.0f || transform.position.x < -20.0f || transform.position.y > 20.0f || transform.position.y < -20.0f)
//        {
//            Destroy(gameObject);
//        }
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
		if (other.gameObject.layer != ignoreThese){
			if (other.gameObject.tag == "Player"){
				gameHandler.TakeDamage(damage);
				Destroy(gameObject);
			}
		}
	}

}
