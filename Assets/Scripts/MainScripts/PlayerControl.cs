using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour, IInteractable
{

    public List<Item> playerInventory = new List<Item>();

    public float movementSpeed;
    public Rigidbody2D rb;

    public float jumpForce = 10f;
    public Transform feet;
    public LayerMask groundLayers;
    public LayerMask enemyLayer;
    private bool m_FacingRight = true; 

    //Player Stats 
    public float Health;
    public float Defense;
    public float Attack;
    public float AttackSpeed;
    public float Luck;


    private IInteractable interactable;
    
    
    float mouseInput;

    private void Start(){
          rb = GetComponent<Rigidbody2D>();
          
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

        if (Input.GetAxisRaw("Horizontal") > 0 && !m_FacingRight) {
            Debug.Log("left");
            //left;
            Flip();
        } else if (Input.GetAxisRaw("Horizontal") < 0 && m_FacingRight) {
            //right
            Debug.Log("right");
            Flip();
        }

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

    void addToInventory(Item pickup) {
        playerInventory.Add(pickup);
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
            interactable = other.GetComponent<IInteractable>();
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


    public void Interact()
    {
        if (interactable != null) {
            interactable.Interact();
        }
    }

    public void StopInteract()
    {

    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
