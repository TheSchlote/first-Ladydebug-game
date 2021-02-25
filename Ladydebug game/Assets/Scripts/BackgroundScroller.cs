using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private GameObject coffeePrefab;
    [SerializeField]
    private GameObject microchipPrefab;

    private GameObject coffee;
    private GameObject microchip;

    private BoxCollider2D collider;
    private Rigidbody2D rb;

    private float height;
    private float scrollSpeed = -2f;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        height = collider.size.y;
        collider.enabled = false;

        rb.velocity = new Vector2(0, scrollSpeed);

        coffee = Instantiate(coffeePrefab, new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)), Quaternion.identity);
        coffee.transform.parent = transform.GetChild(0);
        Destroy(coffee);

        microchip = Instantiate(microchipPrefab, new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)), Quaternion.identity);
        microchip.transform.parent = transform.GetChild(0);
        Destroy(microchip);

        ResetObstacle(0);
    }

    void Update()
    {
        if (transform.GetChild(0).position.y < -height)
        {
            Vector2 resetPosition = new Vector2(0, height * 2f);
            transform.GetChild(0).position = (Vector2)transform.GetChild(0).position + resetPosition;
            ResetObstacle(0);
        }

        if (transform.GetChild(1).position.y < -height)
        {
            Vector2 resetPosition = new Vector2(0, height * 2f);
            transform.GetChild(1).position = (Vector2)transform.GetChild(1).position + resetPosition;
            ResetObstacle(1);
        }
    }
    
    public void ResetObstacle(int child)
    {
        if (!coffee)
        {
            coffee = Instantiate(coffeePrefab, new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)), Quaternion.identity);
            coffee.transform.parent = transform.GetChild(child);
        }
        coffee.transform.localPosition = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));

        if (!microchip)
        {
            microchip = Instantiate(microchipPrefab, new Vector3(Random.Range(-1, 1), Random.Range(-1, 1)), Quaternion.identity);
            microchip.transform.parent = transform.GetChild(child);
        }
        microchip.transform.localPosition = new Vector2(Random.Range(-1, 1), Random.Range(-1, 1));
    }

    public void DestroyCoffee()
    {
        Destroy(coffee);
    }
}
