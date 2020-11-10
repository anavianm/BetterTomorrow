using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
    public int damageDealt;

    public Transform playerTransform;

    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int direction = 1;
        if (playerTransform.position.x < transform.position.x)
        {
            direction = -1;
        }
        transform.position = new Vector2(transform.position.x + (speed * Time.deltaTime * direction), transform.position.y);
    }
}
