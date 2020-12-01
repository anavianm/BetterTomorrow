using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IInteractable
{

    public List<Item> playerInventory = new List<Item>();

    public float movementSpeed;
    public Rigidbody2D rb;

    public float jumpForce = 10f;
    public Transform feet;
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

    private void Start(){
          rb = GetComponent<Rigidbody2D>();
          chest = GameObject.FindGameObjectWithTag("Interactable").GetComponent<Chest>();
          
    }

    private void Update() {
        mouseInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
        }

        if (Input.GetButtonDown("Fire1")) {
           Interact();
        }



    }

    private void FixedUpdate() {
        Vector2 movement = new Vector2(mouseInput * movementSpeed, rb.velocity.y);

        rb.velocity = movement;
    }
    void Jump() {
        Vector2 movement = new Vector2(rb.velocity.x, jumpForce);

        rb.velocity = movement;
    }

    public bool IsGrounded() {
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 2f, groundLayers);

        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 2f, enemyLayer);

        if (groundCheck != null || enemyCheck != null) {
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
            Debug.Log(movementSpeed);
        }

     
        if (other.tag == "Interactable") {
            Debug.Log("here in interactable");
            interactable = other.GetComponent<IInteractable>();
           

            // Debug.Log("added item to player inventory");
        }

        
        
    }

    public void OnTriggerExit2D(Collider2D other)
    {
          if (other.tag == "Interactable") {
              if(interactable != null) {
                    interactable = other.GetComponent<IInteractable>();
                    interactable = null;
              }
            
        }
    }

    private void AddModifiersToPlayer(Item pickup)
    {
           if(pickup.stats.ContainsKey("HP")) {
               Debug.Log("added HP");
               MaxHealth *= pickup.stats["HP"];
           }else if (pickup.stats.ContainsKey("Heal")) {
               HealthOverTime += pickup.stats["Heal"];
           }
    }


    public void Interact()
    {
        if (interactable != null) {
            interactable.Interact();
            // AddModifiersToPlayer();
        }

        
    }

    public void StopInteract()
    {

    }
}
