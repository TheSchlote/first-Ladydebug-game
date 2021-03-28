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
    bool left;
    bool right;
    private void Update()
    {
        float moveDirection = Input.GetAxisRaw("Horizontal");
        if (left)
            moveDirection = -1;
        if (right)
            moveDirection = 1;
        rb.velocity = new Vector2(moveDirection * moveSpeed, 0);
    }

    public void LeftButton()
    {
        left = true;
    }
    public void RightButton()
    {
        right = true;
    }
    public void ButtonReset()
    {
        left = false;
        right = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag) 
        {
            case "Coffee":
                Coffee(collision);
                break;
            case "Obstacle":
                Obstacle();
                break;
            case "Bug":
                Bug(collision);
                break;
            default:
                Debug.Log("Unkown collision");
                break;
        }
    }

    private void Bug(Collider2D collision)
    {
        Destroy(collision.gameObject);
        gameManager.PlayBugSound();
        bugCount += 1;
        bugCountTextbox.text = "Bugs: " + bugCount.ToString();
    }

    private void Obstacle()
    {
        Destroy(gameObject);

        Button LeftButton = GameObject.Find("LeftButton").GetComponent<Button>();
        Button RightButton = GameObject.Find("RightButton").GetComponent<Button>();

        LeftButton.gameObject.SetActive(false);
        RightButton.gameObject.SetActive(false);

        gameManager.YouDiedDisplay();
    }

    private void Coffee(Collider2D collision)
    {
        gameManager.PlayCoffeeSound();
        Destroy(collision.gameObject);
        if (moveSpeed < 10)
            moveSpeed += 1;
        coffeeMeter.value += 1;
        if (coffeeMeter.value == coffeeMeter.maxValue)
        {
            coffeeMeter.value = 0;
            background.SpeedUp();
        }
    }
}

