using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{

	public GameObject pauseMenu;
	public static bool isPaused;

	public AudioMixer mixer;
	public static float volumeLevel = 1.0f;
	private Slider sliderVolumeCtrl;


	void Awake (){
		SetLevel (volumeLevel);
		GameObject sliderTemp = GameObject.FindWithTag("PauseMenuSlider");
		if (sliderTemp != null){
			sliderVolumeCtrl = sliderTemp.GetComponent<Slider>();
			sliderVolumeCtrl.value = volumeLevel;
		}
	}

    void Start()
    {
    	pauseMenu.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
        	if (isPaused) {
        		ResumeGame();
        	} else {
        		PauseGame();
        	}
        }
    }


    public void PauseGame() {
    	pauseMenu.SetActive(true);
    	Time.timeScale = 0f;
    	isPaused = true;
    }

    public void ResumeGame() {
    	pauseMenu.SetActive(false);
    	Time.timeScale = 1f;
    	isPaused = false;
    }

    public void GoToMainMenu(){
    	Time.timeScale = 1f;
    	SceneManager.LoadScene("1. Menu");

    }

    public void QuitGame() {
    	Application.Quit();
    }


	public void SetLevel (float sliderValue){
		mixer.SetFloat("MusicVolume", Mathf.Log10 (sliderValue) * 20);
		volumeLevel = sliderValue;
	} 

}
