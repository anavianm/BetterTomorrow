using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public List<Item> playerInventory = new List<Item>();

    public float movementSpeed;
    public Rigidbody2D rb;

    public float jumpForce = 20f;
    public Transform feet;
    public LayerMask groundLayers;
    public LayerMask enemyLayer;

    //Player Stats 
    public float Health;
    public float Defense;
    public float Attack;
    public float AttackSpeed;
    public float Luck;
    
    
    float mouseInput;

    private void Start(){
          rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        mouseInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && IsGrounded()) {
            Jump();
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
        Collider2D groundCheck = Physics2D.OverlapCircle(feet.position, 0.5f, groundLayers);

        Collider2D enemyCheck = Physics2D.OverlapCircle(feet.position, 0.5f, enemyLayer);

        if (groundCheck != null || enemyCheck != null) {
            return true;
        }

        return false;
    }

    void addToInventory(Item pickup) {
        playerInventory.Add(pickup);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Debug.Log("movement speed",  movementSpeed);
        if (other.gameObject.CompareTag("PickUp")) {
            jumpForce *= (float)1.05;
            other.gameObject.SetActive(false);
            Debug.Log(movementSpeed);
        }
    }
}
