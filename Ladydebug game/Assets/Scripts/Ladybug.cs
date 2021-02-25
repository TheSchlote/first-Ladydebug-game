using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ladybug : MonoBehaviour
{
    public BackgroundScroller background;

    [SerializeField] private Slider coffeeMeter;
    private Rigidbody2D rb;
    private float moveSpeed = 5;
    private void Start()
    {
        coffeeMeter.maxValue = 50;
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
            background.DestroyCoffee();
            moveSpeed += 1;
            coffeeMeter.value = moveSpeed;
            Debug.Log("Drank Coffee! Go Faster!");
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            Debug.Log("You Died!");
        }
    }
}

