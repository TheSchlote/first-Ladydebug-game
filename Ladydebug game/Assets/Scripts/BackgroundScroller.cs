using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField]
    private GameObject coffeePrefab;
    [SerializeField]
    private GameObject microchipPrefab;
    [SerializeField]
    private GameObject lightningBoltPrefab;
    [SerializeField]
    private GameObject bugPrefab;

    private GameObject coffee;
    private GameObject microchip;
    private GameObject lightningBolt;
    private GameObject bug;

    public Dictionary<GameObject, GameObject> obstacles = new Dictionary<GameObject, GameObject>();

    private BoxCollider2D collider;
    private Rigidbody2D rb;

    private float height;
    public float scrollSpeed = -2f;

    private void Start()
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
        obstacles.Add(lightningBoltPrefab, lightningBolt);
        obstacles.Add(bugPrefab, bug);

    }
    private void Update()
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
        List<GameObject> alreadySpawnedPrefabs = new List<GameObject>();
        foreach (GameObject prefab in keys)
        {
            GameObject thingToSpawn;
            if (obstacles.TryGetValue(prefab, out thingToSpawn))
            {
                obstacles[prefab] = Instantiate(prefab, new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f)), Quaternion.identity);
                obstacles[prefab].transform.parent = transform.GetChild(child);
                obstacles[prefab].transform.localPosition = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));

                alreadySpawnedPrefabs.Add(obstacles[prefab]);
                if (alreadySpawnedPrefabs.Count > 1)
                {
                    foreach (GameObject alreadySpawnedPrefab in alreadySpawnedPrefabs)
                    {
                        //there are different types of colliders
                        while (obstacles[prefab].GetComponent<PolygonCollider2D>().IsTouching(alreadySpawnedPrefab.GetComponent<PolygonCollider2D>()))
                        {
                            obstacles[prefab].transform.localPosition = new Vector2(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                        }
                    }
                }
            }
        }
    }

    public void SpeedUp()
    {
        scrollSpeed -= 1;
        rb.velocity = new Vector2(0, scrollSpeed);
    }
}
