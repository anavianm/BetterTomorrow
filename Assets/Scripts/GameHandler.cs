using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour{

	
	private FinalBossScript finalBoss;
	//Player Stats 
	public static float MaxHealth = 100;
	public static float CurrentHealth;
	private static float Defense = 0.5f;
	public static float Attack = 0.1f;
	private static float AttackSpeed;
	private static float Luck;
	public static float HealthOverTime = 0.002f;
	public static float HealingRate;

	public int coins = 0;

	public GameObject healthText;
	public RectTransform healthBar;
	private string sceneName;

	public GameObject coinCounter;

	private PlayerData playerdata;

	private bool firstEnemyDead = false;
	private bool secondEnemyDead = false;

	SpriteRenderer door;


    // Start is called before the first frame update
    void Start(){
		// door = GameObject.FindWithTag("door").GetComponent<SpriteRenderer>;
		// door.setActive(false);
		


		if (GameObject.FindWithTag("Player") != null){
			playerdata = GameObject.FindWithTag("Player").GetComponent<PlayerData>();
		}

		if (GameObject.FindWithTag("EnemyLarge") != null){
			finalBoss = GameObject.FindWithTag("EnemyLarge").GetComponent<FinalBossScript>();
		}
		CurrentHealth = MaxHealth;
		UpdateHealth();
		//Scene ThisScene = SceneManager.GetActiveScene();
		sceneName = SceneManager.GetActiveScene().name;
    }

	void Update () {     // always include a way to quit the game :)
		// if (Input.GetKey("escape")) {
		// 	Application.Quit ();
		// }

		if (CurrentHealth >= MaxHealth){CurrentHealth = MaxHealth;}



		if ((CurrentHealth <= 0) && (sceneName != "Lisa_LoseScreen")) {
				SceneManager.LoadScene("Lisa_LoseScreen");
			}

		// if the scene is the classroom scene and the boss dies, first level bool true
		if ((sceneName == "Classroom Level") && finalBoss.BossHealth == 0){
			firstEnemyDead = true;
			SceneManager.LoadScene("WinterLevel");
			// door.setActive(true);
		}

		if ((sceneName == "WinterLevel") && finalBoss.BossHealth == 0){
			secondEnemyDead = true;
			// door.setActive(true);
		}


		if(secondEnemyDead && firstEnemyDead){
			SceneManager.LoadScene("Lisa_WinScreen");
		}

		//if both are true, then win screen

		if (firstEnemyDead && secondEnemyDead){
			SceneManager.LoadScene("Lisa_WinScreen");
		}

	}

	void FixedUpdate()
	{
		addHealthSmall();
		UpdateHealth();
	}

	void addHealthSmall()
	{
		if(CurrentHealth != MaxHealth)
		{
			CurrentHealth += HealthOverTime;
		}
		
	}
		


	public void TakeDamage(float damage){
		// Debug.Log(damage);
		// damage -= Defense;
		CurrentHealth -= damage;
		UpdateHealth();
	}


	public void UpdateHealth(){
		Text healthTextB = healthText.GetComponent<Text>();
		healthTextB.text =  ("" + CurrentHealth + " / " + MaxHealth);
	}

	public void UpdateHealthBar(){
		healthBar.sizeDelta = new Vector2(CurrentHealth * 2, healthBar.sizeDelta.y);
	}

	public void UpdateCoinCounter() {
		Text coinText = coinCounter.GetComponent<Text>();
		coinText.text = ("Coins" + 	coins);
	}

	public float getAttackBuff()
	{
		return Attack;
	}

	public void setAttackBuff(float attack)
	{
		Attack += attack;
	}

	public void setFlatHealthIncrease(float health)
	{
		CurrentHealth += health;
		UpdateHealth();
	}

	public void setPercentageHealth(float health)
	{
		float decreaseHealth = CurrentHealth * health;
		Debug.Log(health);
		CurrentHealth += decreaseHealth;
		UpdateHealth();
	}	

	public void setMaxHealthIncrease(float maxHealth)
	{
		MaxHealth *= maxHealth;
	}

	public void setAttackCooldown(float cooldown)
	{
		playerdata.shotCooldown *= cooldown;
	}

	public void changeHealthOverTime(float hot)
	{
		HealthOverTime = hot;
	}






	public void StartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void RestartGame(){
		SceneManager.LoadScene("1. Menu");
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void Credits(){
		SceneManager.LoadScene("Credits");
	}

}
