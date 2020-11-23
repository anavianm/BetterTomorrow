using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{

    public static AudioClip objectPickUpSound;
    static AudioSource audioSrc;
    // Start is called before the first frame update
    void Start () 
    {
        objectPickUpSound = Resources.Load<AudioClip>("PickUpSound");

        audioSrc = GetComponent<AudioSource> ();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {   
        if(other.gameObject.tag.Equals("Player")){
                audioSrc.PlayOneShot (objectPickUpSound);
        }     
       
    }
}
