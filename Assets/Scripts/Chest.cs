using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
	public GameObject ChestOpen;
	public GameObject ChestClosed;
	public GameObject messageToOpen;

    //[SerializeField]
    //private SpriteRenderer spriteRenderer;

    //[SerializeField]
    //private Sprite openSprite, closedSprite;
    private bool isOpen;

    //public Transform chest;

    ItemDatabase db;
    Player player;

    public Item item;

    GameObject myObject;
    GameObject temp;

    public GameObject water, friendship_bracelet, food, magnifying_glass, inhaler, medicine, cigarettes;

    Vector2 objectPlacement = new Vector2(0,0);
    Transform objectTransform;

    Transform pickupParent;

    Vector3 mousePos;


    void Start() {
        db = GameObject.FindGameObjectWithTag("Player").GetComponent<ItemDatabase>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		ChestOpen.SetActive(false);
		ChestClosed.SetActive(true);
		messageToOpen.SetActive(false);
    }

    void Update()
    {
        mousePos = Input.mousePosition;
    }


	public void TurnOnMessage(){
		messageToOpen.SetActive(true);
	}

	public void TurnOffMessage(){
		messageToOpen.SetActive(false);
	}


    public void Interact()
    {
        if (isOpen) {
            StopInteract();
        } else {
            isOpen = true;
            //spriteRenderer.sprite = openSprite;
			ChestOpen.SetActive(true);
			ChestClosed.SetActive(false);
			messageToOpen.SetActive(false);
            item = db.getRandomItem();
            Debug.Log(item.title);
            if(item.title == "Magnifying Glass"){
                Instantiate(magnifying_glass, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y), Quaternion.identity);
            }else if(item.title == "Friendship bracelet"){
                Instantiate(friendship_bracelet, new Vector3(gameObject.transform.position.x + 5, gameObject.transform.position.y), Quaternion.identity);
            }else if(item.title == "Food"){
                Instantiate(food, new Vector2(gameObject.transform.position.x + 5, gameObject.transform.position.y), Quaternion.identity);
            }else if(item.title == "Water"){
                Instantiate(water, new Vector2(gameObject.transform.position.x + 5, gameObject.transform.position.y) , Quaternion.identity);
            }else if(item.title == "Inhaler"){
                Instantiate(inhaler, new Vector2(gameObject.transform.position.x + 5, gameObject.transform.position.y) , Quaternion.identity);
            }else if(item.title == "Medicine"){
                Instantiate(medicine, new Vector2(gameObject.transform.position.x + 5, gameObject.transform.position.y) , Quaternion.identity);
            }else if(item.title == "Cigarettes"){
                Instantiate(cigarettes, new Vector2(gameObject.transform.position.x + 5, gameObject.transform.position.y) , Quaternion.identity);
            }
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
