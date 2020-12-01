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

    public Transform chest;

    ItemDatabase db;
    Player player;

    public Item item;

    GameObject myObject;
    GameObject temp;

    Vector2 objectPlacement = new Vector2(0,0);
    Transform objectTransform;

    Transform pickupParent;

    Vector3 mousePos;



    

    void Start() {
        db = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDatabase>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        mousePos = Input.mousePosition;
    }

    public void Interact()
    
    {
        if (isOpen) {
            StopInteract();
        } else {
            isOpen = true;
            spriteRenderer.sprite = openSprite;
            item = db.getRandomItem();
            Debug.Log(item.title);
            myObject = new GameObject(item.title);
            temp = Instantiate(myObject);
            temp.SetActive(true);
            temp.transform.position = new Vector2(0,0);
            player.addToInventory(item);

        }

    }

    public Item getItemFromChest()
    {
        Debug.Log("in getItemFromChest");
        Debug.Log(item.title);
        return item;
    }

    public void StopInteract()
    {

    }
}
