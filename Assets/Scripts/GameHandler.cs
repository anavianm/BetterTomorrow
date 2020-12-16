using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour{


	//Player Stats 
	public static double MaxHealth = 100;
	public static double CurrentHealth = 100;
	private static double Defense;
	private static double Attack;
	private static double AttackSpeed;
	private static double Luck;
	public static double HealthOverTime;
	public static double HealingRate;

	public GameObject healthText;
	private string sceneName; 


    // Start is called before the first frame update
    void Start(){
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


	public void TakeDamage(double damage){
		CurrentHealth -= damage;
		UpdateHealth();
	}


	public void UpdateHealth(){
		Text healthTextB = healthText.GetComponent<Text>();
		healthTextB.text = "Current Health: " + CurrentHealth + "\n Max Health: " + MaxHealth;
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
