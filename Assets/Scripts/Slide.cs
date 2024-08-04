using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition += new Vector3(speed * Time.deltaTime, 0, 0);
    }
}
