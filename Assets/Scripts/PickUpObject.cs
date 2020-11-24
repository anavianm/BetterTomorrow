using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour, IInteractable
{

    public static AudioClip objectPickUpSound;
    static AudioSource audioSrc;

    bool isPickedUp;
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

    public void Interact()
    {   
        if(!isPickedUp){
            // gameObject.setActive = false;
        }
    }

    public void StopInteract()
    {

    }
}
