using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{

    public int health;
    public int damageDealt;

    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        damageDealt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Projectile")
        {
            health += -10;
            Destroy(collider.gameObject);
        }
    }
}
