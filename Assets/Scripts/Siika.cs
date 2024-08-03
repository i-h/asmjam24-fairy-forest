using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Siika : MonoBehaviour
{
    public GameObject hud;

    private void OnTriggerEnter(Collider c){
        if(c.CompareTag("Player")) siika();
    }

    void siika()
    {
        hud.SetActive(true);
    }
}
