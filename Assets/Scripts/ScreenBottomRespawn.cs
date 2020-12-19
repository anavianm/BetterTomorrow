using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScreenBottomRespawn : MonoBehaviour
{
	public GameHandler gameHandler;
	public Transform playerPos;
	public Transform playerSpawn;
	public float damage = 10;


    // Start is called before the first frame update
    void Start()
    {
		playerPos = GameObject.FindWithTag("Player").GetComponent<Transform>();
		gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
    }

    // Update is called once per frame
    void Update()
    {
		if (playerPos != null){
			if (transform.position.y >= playerPos.position.y){ 
				//instantiate a particle effect
				Debug.Log("I am going back to start");

				gameHandler.TakeDamage(damage);

				Vector3 playerSpawnTemp = new Vector3(playerSpawn.position.x, playerSpawn.position.y, playerPos.position.z);
				playerPos.position = playerSpawnTemp; 
			}
		}
    }


//	void OnTriggerEnter2D(Collider2D other){
//		if (other.gameObject.tag == "Player"){
//			playerPos = playerSpawn;
//		}
//	}



}
