using UnityEngine;
using UnityEngine.UI;

public class Ladybug : MonoBehaviour
{
    public BackgroundScroller background;
    public GameManager gameManager;

    [SerializeField] private Slider coffeeMeter;
    [SerializeField] private Text bugCountTextbox;
    private Rigidbody2D rb;
    private float moveSpeed = 1;
    private int bugCount = 0;

    private void Start()
    {
        coffeeMeter.maxValue = 5;
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
            gameManager.PlayCoffeeSound();
            Destroy(collision.gameObject);
            moveSpeed += 1;
            coffeeMeter.value += 1;
            if(coffeeMeter.value == coffeeMeter.maxValue)
            {
                coffeeMeter.value = 0;
                background.SpeedUp();
            }
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            gameManager.YouDiedDisplay();
        }

        if (collision.gameObject.CompareTag("Bug"))
        {
            Destroy(collision.gameObject);
            gameManager.PlayBugSound();
            bugCount += 1;
            bugCountTextbox.text = "Bugs: " + bugCount.ToString();
        }
    }
}

