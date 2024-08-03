using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SiikaWatcher : MonoBehaviour
{
    [SerializeField] private AudioSource siikaSource;
    private bool triggered = false;
    
    void Update()
    {
        if(!triggered && Physics.Raycast(transform.position, transform.forward, out RaycastHit hit)){
            if(hit.collider.CompareTag("Siika")){
                PlaySiika();
                triggered = true;
            }
        }
    }

    private void PlaySiika(){
        Debug.Log("SIIKAHA SE SIELLÄ");
        siikaSource.Play();

    }
}
