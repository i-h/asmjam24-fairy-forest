using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SmoothRotate : MonoBehaviour
{
 
    public float time = 1.0f;
 
    private float timer = 0.0f;
    private bool isRotating = false;
    private Vector3 axis = Vector3.forward; // Assuming rotation around Z-axis
    public float currentAngle = 0.0f;

    public AnimationCurve Leftcurve;
    public AnimationCurve Rightcurve;

    private Quaternion startRotation;
    private Quaternion endRotation;
    private AnimationCurve currentCurve;

    [SerializeField] private KeyCode rotateKey = KeyCode.E;

    // Start is called before the first frame update
    void Start()
    {
        currentAngle = transform.rotation.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(rotateKey) && !isRotating)
        {
            Rotate();
        }

        if (isRotating)
        {
            timer += Time.deltaTime;
            float t = timer / time;
            if (t > 1.0f) t = 1.0f;

            float curveValue = currentCurve.Evaluate(t);
            transform.rotation = Quaternion.Lerp(startRotation, endRotation, curveValue);

            if (t >= 1.0f)
            {
                isRotating = false;
                timer = 0.0f;
            }

            
        }
    }

    public void Rotate()
    {
        if(isRotating) return;
        // Retrieve the current z-axis angle
        currentAngle = transform.rotation.eulerAngles.z;
        // If the angle is 0, call the RotateLeft() method
        if (Mathf.Approximately(currentAngle, 0.0f))
        {
            RotateLeft();
        }
        // If the angle is 180, call the RotateRight() method
        else if (Mathf.Approximately(currentAngle, 180.0f))
        {
            RotateRight();
        }
    }

    public void RotateRight()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(0, 0, 0);
        currentCurve = Rightcurve;
        isRotating = true;
    }

    public void RotateLeft()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(0, 0, 180);
        currentCurve = Leftcurve;
        isRotating = true;
    }
}
