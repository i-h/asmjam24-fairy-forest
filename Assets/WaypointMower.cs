using System.Collections.Generic;
using UnityEngine;

public class WaypointMover : MonoBehaviour
{
    public GameObject keiju; // Pelihahmo, joka liikkuu
    public List<Transform> waypoints; // Lista waypointeista
    public float moveSpeed = 5f; // Liikkumisnopeus
    public float detectionRadius = 10f; // Pelaajan havaitsemisetäisyys
    public float currentRadius = 0f; // Tämänhetkinen etäisyys pelaajasta

    private Transform player; // Viittaus pelaajaan
    private int currentWaypointIndex = 0; // Tämänhetkinen waypoint-indeksi

    void Start()
    {
        // Etsi pelaaja tagilla "Player"
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Lisää kaikki waypointit listalle, jos ei ole määritetty
        if (waypoints.Count == 0)
        {
            waypoints = new List<Transform>();
            foreach (GameObject waypoint in GameObject.FindGameObjectsWithTag("Waypoint"))
            {
                waypoints.Add(waypoint.transform);
            }
        }
    }

    void Update()
    {
        currentRadius = Vector3.Distance(keiju.transform.position, player.position);
        // Tarkista etäisyys pelaajaan
        if (Vector3.Distance(keiju.transform.position, player.position) <= detectionRadius)
        {
            MoveTowardsWaypoint();
        }
    }

    void MoveTowardsWaypoint()
    {
        if (waypoints.Count == 0)
            return;

        Transform targetWaypoint = waypoints[currentWaypointIndex];
        Vector3 direction = targetWaypoint.position - keiju.transform.position;
        keiju.transform.position = Vector3.MoveTowards(keiju.transform.position, targetWaypoint.position, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(keiju.transform.position, targetWaypoint.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }
    }
}