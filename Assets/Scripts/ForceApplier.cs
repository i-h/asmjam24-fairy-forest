using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceApplier : MonoBehaviour
{
    [SerializeField] private float _sigma = 10f;
    [SerializeField] private float _rho = 28f;
    [SerializeField] private float _beta = 8f/3f;
    [SerializeField] private float _timeStep = 0.01f;
    
    // Update is called once per frame
    void Update()
    {
        ApplyLorenzForce();
    }

    private void ApplyLorenzForce()
    {
        var position = transform.position;
        var dt = Time.deltaTime * _timeStep;
        float dx = _sigma * (position.y - position.x) * dt;
        float dy = (position.x * (_rho - position.z) - position.y) * dt;
        float dz = (position.x * position.y - _beta * position.z) * dt;

        position.x += dx;
        position.y += dy;
        position.z += dz;

        transform.position = position;
    }
}
