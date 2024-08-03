using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
  
    public Transform player;
    public float pushForce = 1.0f;
    public AnimationCurve pushCurve;
    public float pushTime = 0.2f;
    
    public float currentDistance = 0.0f;
    public float pushDistance = 5.0f;

    private Rigidbody playerRb;
    private float pushTimer = 0.0f;

    void Start()
    {
        if (player != null)
        {
            playerRb = player.GetComponent<Rigidbody>();
            if (playerRb == null)
            {
                Debug.LogError("Player object does not have a Rigidbody component.");
            }
        }
    }

    void Update()
    {
        LookPlayer();
        GetDistance();
    }

    private void LookPlayer()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    private void GetDistance()
    {
        if (player != null)
        {
            currentDistance = Vector3.Distance(player.position, transform.position);

            if (currentDistance < pushDistance && playerRb != null)
            {
                PushPlayer();
            }
        }
    }

    private void PushPlayer()
    {
        pushTimer += Time.deltaTime;
        if (pushTimer > pushTime)
        {
            pushTimer = pushTime;
        }

        float curveValue = pushCurve.Evaluate(pushTimer / pushTime);
        Vector3 pushDirection = (player.position - transform.position).normalized;
        playerRb.AddForce(pushDirection * pushForce * curveValue, ForceMode.Impulse);
    }
}
