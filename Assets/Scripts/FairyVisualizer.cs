using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class FairyVisualizer : MonoBehaviour
{
    [SerializeField] private Volume volume;
    public void Visualize(float bend){
        volume.weight = Mathf.Lerp(0, 1, bend);
    }
}
