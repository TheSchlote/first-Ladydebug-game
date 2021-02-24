using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladybug : MonoBehaviour
{
    private Rigidbody2D rb;
    private float moveSpeed = 5;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveDirection * moveSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coffee"))
        {
            moveSpeed += 1;
            Debug.Log("Drank Coffee! Go Faster!");
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            Debug.Log("You Died!");
        }
    }
}

