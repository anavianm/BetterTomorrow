using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossScript : MonoBehaviour {

	//public float speed = 4f;
	public Animator anim;
	private Transform target;
	public float damage = 2;
	public GameObject shockwave;
	private bool playerHurt = false;
	
	private float stompTimer = 0;
	private float stompTime = 20.0f;

	private Renderer rend;
	private Renderer rendPlayer;
	private GameHandler gameHandlerObj;

	public float attackRange = 6;
	private bool isAttacking = false;

	void Start () {
		shockwave.SetActive(false);
		rend = GetComponentInChildren<Renderer>();
		anim = GetComponentInChildren<Animator>();

		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			target = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
			rendPlayer = GameObject.FindGameObjectWithTag ("Player").GetComponentInChildren<Renderer>();
		}
		
		
		GameObject gameHandlerLocation = GameObject.FindWithTag ("GameHandler");
		if (gameHandlerLocation != null) {
			gameHandlerObj = gameHandlerLocation.GetComponent<GameHandler> ();
		}
	}

	void Update () {
		if ((target != null) && (Vector2.Distance (transform.position, target.position) <= attackRange)){
			isAttacking = true;
		}	
	}

	void FixedUpdate(){
		if ((target != null) && (isAttacking == true)){
			//transform.position = Vector2.MoveTowards (transform.position, target.position, speed * Time.deltaTime);
			stompTimer += 0.1f;
			if (stompTimer >= stompTime){
				StartCoroutine(KingSlimeStomp());
				stompTimer = 0;
			}
		}
		
		if ((target != null) && (playerHurt == true)){
			StartCoroutine(PlayerColorHit());
			HurtPlayer();
		}
		
	}


	IEnumerator KingSlimeStomp(){
		anim.SetBool("Hop", true);
		shockwave.SetActive(true);
		playerHurt = true;
		yield return new WaitForSeconds(0.1f);
		playerHurt = false;
		yield return new WaitForSeconds(0.7f);
		anim.SetBool("Hop", false);
		shockwave.SetActive(false);
	}


	IEnumerator PlayerColorHit(){
		rendPlayer.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
		yield return new WaitForSeconds(0.5f);
		rendPlayer.material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	}
	
	public void HurtPlayer(){
		gameHandlerObj.TakeDamage(damage);
	}
	

   //to help see the attack sphere in editor:
   void OnDrawGizmosSelected(){
        Transform attackPoint = gameObject.transform;
		if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
   }

   public void TakeDamage(float damage)
   {

   }

//	public void OnTriggerEnter2D(Collider2D other){
//		if (other.gameObject.tag == "Player") {
//			rendPlayer = other.gameObject.GetComponentInChildren<Renderer>();
//			//EnemyLives -= EnemyLives;
//			rendPlayer.material.color = new Color(2.4f, 0.9f, 0.9f, 0.5f);
//			gameHandlerObj.playerGetHit(damage);
//			//Destroy(gameObject);
//		}
//	}

}