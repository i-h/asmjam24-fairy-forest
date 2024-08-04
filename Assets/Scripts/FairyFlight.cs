using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FairyFlight : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    private int currentTarget = 0;
    [SerializeField] private float speed = 2f;
    private AudioSource audio;
    
    private void OnTriggerEnter(Collider c){
        if(c.CompareTag("Player")) {
            currentTarget++;
            audio.Play();
        }
    }
    private void Start(){
        transform.position = waypoints[0].position;
        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (waypoints.Length == 0) return;

        Transform targetWaypoint = waypoints[currentTarget];
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);
    }
}
