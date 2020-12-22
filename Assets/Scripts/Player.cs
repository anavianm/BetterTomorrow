using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour, IInteractable
{


	private GameHandler gameHandler;

	public Animator animator;
	private bool m_FacingRight = true; 

    public List<Item> playerInventory = new List<Item>();

    public float movementSpeed;
    public Rigidbody2D rb;

    public float jumpForce = 35f;
    public Transform GroundCheck;
    public LayerMask groundLayers;
    public LayerMask enemyLayer;

    //Player Stats 
    public double MaxHealth;
    public double CurrentHealth;
    public double Defense;
    public double Attack;
    public double AttackSpeed;
    public double Luck;
    public double HealthOverTime;


    private IInteractable interactable;
    
    Chest chest;
    Item currentItem;
    
    float mouseInput;


	public GameObject item1;
	public GameObject item2;
	public GameObject item3;
	public GameObject item4;
	public GameObject item5;
	public GameObject item6;
	public GameObject item7;

	public GameObject item1Text;
	public GameObject item2Text;
	public GameObject item3Text;
	public GameObject item4Text;
	public GameObject item5Text;
	public GameObject item6Text;
	public GameObject item7Text;


	private int item1num;
	private int item2num;
	private int item3num;
	private int item4num;
	private int item5num;
	private int item6num;
	private int item7num;


	void Awake(){

		item1.SetActive(false);
		item2.SetActive(false);
		item3.SetActive(false);
		item4.SetActive(false);
		item5.SetActive(false);
		item6.SetActive(false);
		item7.SetActive(false);
		item1Text.SetActive(false);
		item2Text.SetActive(false);
		item3Text.SetActive(false);
		item4Text.SetActive(false);
		item5Text.SetActive(false);
		item6Text.SetActive(false);
		item7Text.SetActive(false);

	}



    private void Start(){
		gameHandler = GameObject.FindWithTag("GameHandler").GetComponent<GameHandler>();
		animator = gameObject.GetComponentInChildren<Animator>();  
		rb = GetComponent<Rigidbody2D>();
        chest = GameObject.FindGameObjectWithTag("Interactable").GetComponent<Chest>();
          
    }

    private void Update() {
        mouseInput = Input.GetAxisRaw("Horizontal");

		animator.SetFloat("Speed", Mathf.Abs(mouseInput));

		//IsGrounded();

        //if (Input.GetButtonDown("Jump")) {
		if (Input.GetButtonDown("Jump") && IsGrounded()) {
			Jump();
			animator.SetTrigger("Jump");
        }

        if (Input.GetButtonDown("Chests")) {
           Interact();
        }
    }

    private void FixedUpdate() {
        Vector2 movement = new Vector2(mouseInput * movementSpeed, rb.velocity.y);
        rb.velocity = movement;

		if (Input.GetAxisRaw("Horizontal") > 0 && !m_FacingRight) {
			//Debug.Log("left");
			//left;
			Flip();
		} else if (Input.GetAxisRaw("Horizontal") < 0 && m_FacingRight) {
			//right
			//Debug.Log("right");
			Flip();
		}
    }


    void Jump() {
		rb.velocity = Vector2.up * jumpForce;
		//Vector2 movement = new Vector2(rb.velocity.x, jumpForce);
        //rb.velocity = movement;
    }

    public bool IsGrounded() {
		Collider2D groundCheck = Physics2D.OverlapCircle(GroundCheck.position, 2f, groundLayers);

		Collider2D enemyCheck = Physics2D.OverlapCircle(GroundCheck.position, 2f, enemyLayer);

        if (groundCheck != null || enemyCheck != null) {
			Debug.Log("I can jump now!");
			return true;
        }
        return false;
    }

    public void addToInventory(Item pickup) {
        playerInventory.Add(pickup);
        AddModifiersToPlayer(pickup);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("movement speed",  movementSpeed);
        if (other.gameObject.CompareTag("PickUp")) {
            
            jumpForce *= (float)1.05;
            other.gameObject.SetActive(false);
            //Debug.Log(movementSpeed);

			if ((other.gameObject.name == "Food(Clone)") || (other.gameObject.name == "item1")){
				item1.SetActive(true);
				item1Text.SetActive(true);
				item1num += 1;
				Text item1TextB = item1Text.GetComponent<Text>();
				item1TextB.text = ("" + item1num);
				gameHandler.setMaxHealthIncrease(1.10f);
			}
			else if ((other.gameObject.name == "Cigarette(Clone)") || (other.gameObject.name == "item2")){
				item2.SetActive(true);
				item2Text.SetActive(true);
				item2num += 1;
				Text item2TextB = item2Text.GetComponent<Text>();
				item2TextB.text = ("" + item2num);
				gameHandler.setAttackBuff(0.15f);
				gameHandler.setPercentageHealth(-0.5f);

			}
			else if ((other.gameObject.name == "FriendshipBracelet(Clone)") || (other.gameObject.name == "item3")){
				item3.SetActive(true);
				item3Text.SetActive(true);
				item3num += 1;
				Text item3TextB = item3Text.GetComponent<Text>();
				item3TextB.text = ("" + item3num);
				jumpForce += 10;
			}
			else if ((other.gameObject.name == "Inhaler(Clone)") || (other.gameObject.name == "item4")){
				item4.SetActive(true);
				item4Text.SetActive(true);
				item4num += 1;
				Text item4TextB = item4Text.GetComponent<Text>();
				item4TextB.text = ("" + item4num);
				movementSpeed += 3;
			}
			else if ((other.gameObject.name == "MagnifyingGlass(Clone)") || (other.gameObject.name == "item5")){
				item5.SetActive(true);
				item5Text.SetActive(true);
				item5num += 1;
				Text item5TextB = item5Text.GetComponent<Text>();
				item5TextB.text = ("" + item5num);
				gameHandler.setAttackCooldown(.5f);
				
			}
			else if ((other.gameObject.name == "medicine(Clone)") || (other.gameObject.name == "item6")){
				item6.SetActive(true);
				item6Text.SetActive(true);
				item6num += 1;
				Text item6TextB = item6Text.GetComponent<Text>();
				item6TextB.text = ("" + item6num);
				gameHandler.setFlatHealthIncrease(30);
			}
			else if ((other.gameObject.name == "Water(Clone)") || (other.gameObject.name == "item7")){
				item7.SetActive(true);
				item7Text.SetActive(true);
				item7num += 1;
				Text item7TextB = item7Text.GetComponent<Text>();
				item7TextB.text = ("" + item7num);
				gameHandler.changeHealthOverTime(.0002f);
			}
				

        }

     
        if (other.tag == "Interactable") {
            //Debug.Log("here in interactable");
            interactable = other.GetComponent<IInteractable>();
			//assumes all intractibles are chests:
			other.GetComponent<Chest>().TurnOnMessage();
            // Debug.Log("added item to player inventory");
        }
    }

    public void OnTriggerExit2D(Collider2D other){
          if (other.tag == "Interactable") {
              if(interactable != null) {
                    interactable = other.GetComponent<IInteractable>();
                    interactable = null;
				//assumes all intractibles are chests:
				other.GetComponent<Chest>().TurnOffMessage();
              }
        }
    }

    private void AddModifiersToPlayer(Item pickup){
           if(pickup.stats.ContainsKey("HP")) {
               Debug.Log("added HP");
               MaxHealth *= pickup.stats["HP"];
           }else if (pickup.stats.ContainsKey("Heal")) {
               HealthOverTime += pickup.stats["Heal"];
           }
    }


    public void Interact(){
        if (interactable != null) {
            interactable.Interact();
            // AddModifiersToPlayer();
        }
    }

    public void StopInteract(){

    }


	private void Flip(){
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

}
