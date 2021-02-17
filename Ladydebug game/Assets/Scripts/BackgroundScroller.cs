using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public BoxCollider2D collider;
    public Rigidbody2D rb;

    private float height;
    private float scrollSpeed = -2f;

    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();

        height = collider.size.y;
        collider.enabled = false;

        rb.velocity = new Vector2(0, scrollSpeed);
        ResetObstacle();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -height)
        {
            Vector2 resetPosition = new Vector2(0, height * 2f);
            transform.position = (Vector2)transform.position + resetPosition;
            ResetObstacle();
        }
    }

    private void ResetObstacle()
    {
        transform.GetChild(0).localPosition = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
        transform.GetChild(1).localPosition = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
    }
}
