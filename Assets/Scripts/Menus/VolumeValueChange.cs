using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VolumeValueChange : MonoBehaviour
{
    private AudioSource audioSrc;
    private float musicVolume = 1f;

    void Start() {
    	audioSrc = GetComponent<AudioSource>();
    }

    void Update() {
    	audioSrc.volume = musicVolume;

    	// Stop Main Music when WinterLevel
    	if (SceneManager.GetActiveScene().name == "WinterLevel")
         {
             Destroy(this.gameObject);
         }


    }

    public void SetVolume(float vol) {
    	musicVolume = vol;
    }

    // private AudioSource _audioSource;
     private void Awake()
     {
         DontDestroyOnLoad(transform.gameObject);
         // audioSrc = GetComponent<AudioSource>();
     }
 
     public void PlayMusic()
     {
         if (audioSrc.isPlaying) return;
         audioSrc.Play();
     }
 
     public void StopMusic()
     {
         audioSrc.Stop();
     }

}
