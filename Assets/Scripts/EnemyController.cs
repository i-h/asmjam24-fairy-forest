using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    public float speed;

    private GameObject destination;

    // Start is called before the first frame update
    void Start()
    {
        destination = pointA;
    }

    // Update is called once per frame
    void Update()
    {
        // this should surely be done using events or whatever the unity equivalent is
        if (transform.position == destination.transform.position)
            destination = destination == pointA ? destination = pointB : destination = pointA;

        Move();
    }

    void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination.transform.position, speed * Time.deltaTime);
    }
}
