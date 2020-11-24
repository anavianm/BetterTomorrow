using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private Sprite openSprite, closedSprite;
    private bool isOpen;
    ItemDatabase db;
    

    void Start() {

        //  db = FindObjectOfType<ItemDatabase>();
    }

    public void Interact()
    
    {
        if (isOpen) {
            StopInteract();
        } else {
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            db.GetComponent<ItemDatabase>().getRandomItem();
        }

    }

    public void StopInteract()
    {

    }
}
