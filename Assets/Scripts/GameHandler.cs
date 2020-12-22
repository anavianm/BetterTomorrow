using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour{


	//Player Stats 
	public static float MaxHealth = 100;
	public static float CurrentHealth;
	private static float Defense;
	private static float Attack;
	private static float AttackSpeed;
	private static float Luck;
	public static float HealthOverTime;
	public static float HealingRate;

	public GameObject healthText;
	public RectTransform healthbar;
	private string sceneName;


    // Start is called before the first frame update
    void Start(){
		CurrentHealth = MaxHealth;
		UpdateHealth();
		//Scene ThisScene = SceneManager.GetActiveScene();
		sceneName = SceneManager.GetActiveScene().name;
    }

	void Update () {     // always include a way to quit the game :)
		if (Input.GetKey("escape")) {
			Application.Quit ();
		}

		if (CurrentHealth >= MaxHealth){CurrentHealth = MaxHealth;}



		if ((CurrentHealth <= 0) && (sceneName != "EndLose")) {
				SceneManager.LoadScene("EndLose");
			}

//		if (win condition){
//			SceneManager.LoadScene("EndWin");
//		}

	}
		


	public void TakeDamage(float damage){
		CurrentHealth -= damage;
		UpdateHealth();
	}


	public void UpdateHealth(){
		Text healthTextB = healthText.GetComponent<Text>();
		healthTextB.text =  ("" + CurrentHealth + " / " + MaxHealth);
	}

	public void UpdateHealthBar(){
		healthbar.sizeDelta = new Vector2(CurrentHealth * 2, healthbar.sizeDelta.y);
	}






	public void StartGame(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void RestartGame(){
		SceneManager.LoadScene("MainMenu");
	}

	public void QuitGame(){
		Application.Quit ();
	}

	public void Credits(){
		SceneManager.LoadScene("Credits");
	}

}
