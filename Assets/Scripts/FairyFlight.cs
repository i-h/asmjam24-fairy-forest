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
    private Transform player;
    
    private void OnTriggerEnter(Collider c){
        if(c.CompareTag("Player")) {
            currentTarget++;
            if(currentTarget >= waypoints.Length) {
                Disappear();
                GameManager.WinGame();
            }
            audio.Play();
        }
    }
    private void Start(){
        transform.position = waypoints[0].position;
        audio = GetComponent<AudioSource>();
        player = GameObject.FindWithTag("Player").transform;
    }
    void Update()
    {
        if (waypoints.Length == 0 || currentTarget >= waypoints.Length) return;

        Transform targetWaypoint = waypoints[currentTarget];

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

        if(Vector3.SqrMagnitude(targetWaypoint.position - transform.position) == 0){
            transform.LookAt(player, transform.position.y > 0 ? Vector3.up : Vector3.down);
        } else {
            transform.LookAt(targetWaypoint, transform.position.y > 0 ? Vector3.up : Vector3.down);
        }
    }
    private void Disappear(){
        Destroy(gameObject);
    }
}
