using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseLadydebug : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float speed = 1f;

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("Ladydebug"))
        target = GameObject.FindGameObjectWithTag("Ladydebug").GetComponent<Transform>();
    }

    void Update()
    {
        if(target)
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
    }
}
