using System;
using System.Collections.Generic;
using System.Linq;
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
        if (transform.GetChild(0).position.y < -height )
        {
            MovePictures(0);
        }
        if(transform.GetChild(1).position.y < -height)
        {
            MovePictures(1);
        }
    }

    private void MovePictures(int id)
    {
        Vector2 resetPosition = new Vector2(0, height * 2f);
        transform.GetChild(id).position = (Vector2)transform.GetChild(id).position + resetPosition;
        if (transform.GetChild(id).childCount > 0)
        {
            foreach (Transform child in transform.GetChild(id))
            {
                Destroy(child.gameObject);
            }
        }
        ResetObstacle(id);
    }

    public void ResetObstacle(int child)
    {
        List<GameObject> keys = new List<GameObject>(obstacles.Keys);
        foreach (GameObject prefab in keys)
        {
            GameObject thingToSpawn;
            if (obstacles.TryGetValue(prefab, out thingToSpawn))//TODO: see if this is actually what I want. This works but i dont know why??
            {
                obstacles[prefab] = Instantiate(prefab, new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1)), Quaternion.identity);
                obstacles[prefab].transform.parent = transform.GetChild(child);
                obstacles[prefab].transform.localPosition = new Vector2(UnityEngine.Random.Range(-1, 1), UnityEngine.Random.Range(-1, 1));
            }
        }
    }

    public void DestroyCoffee()//TODO: Rework this!
    {
        Destroy(coffee);
    }
}
