using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private GameObject coffeePrefab;
    [SerializeField]
    private GameObject microchipPrefab;

    private GameObject coffee;
    private GameObject microchip;

    public Dictionary<GameObject, GameObject> obstacles = new Dictionary<GameObject, GameObject>();

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

        AssignObstaclePrefabs();
    }

    private void AssignObstaclePrefabs()
    {
        obstacles.Add(coffeePrefab, coffee);
        obstacles.Add(microchipPrefab, microchip);
    }
    void Update()
    {
        if (PictureIsReadyToBeReset(0))
        {
            ResetPicture(0);
        }
        if (PictureIsReadyToBeReset(1))
        {
            ResetPicture(1);
        }
    }

    private bool PictureIsReadyToBeReset(int pictureID)
    {
        return transform.GetChild(pictureID).position.y < -height;
    }

    private void ResetPicture(int id)
    {
        Vector2 resetPosition = new Vector2(0, height * 2f);
        transform.GetChild(id).position = (Vector2)transform.GetChild(id).position + resetPosition;
        DestroyAllChildren(id);
        ResetObstacle(id);
    }

    private void DestroyAllChildren(int id)
    {
        if (transform.GetChild(id).childCount > 0)
        {
            foreach (Transform child in transform.GetChild(id))
            {
                Destroy(child.gameObject);
            }
        }
    }

    public void ResetObstacle(int child)
    {
        List<GameObject> keys = new List<GameObject>(obstacles.Keys);
        foreach (GameObject prefab in keys)
        {
            GameObject thingToSpawn;
            if (obstacles.TryGetValue(prefab, out thingToSpawn))
            {
                obstacles[prefab] = Instantiate(prefab, new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)), Quaternion.identity);
                obstacles[prefab].transform.parent = transform.GetChild(child);
                obstacles[prefab].transform.localPosition = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
            }
        }
    }
}
