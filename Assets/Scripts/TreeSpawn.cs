using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreeSpawn : MonoBehaviour
{
    [SerializeField] private int validationInt;
    [SerializeField] private Transform upTree;
    [SerializeField] private Transform downTree;

    private void OnValidate(){
        UpdateTrees();
    }
    private void UpdateTrees(){
        RaycastHit hit;
        Vector3 castPosition = transform.position;
        if(upTree && Physics.Raycast(castPosition + Vector3.up * 100, Vector3.down, out hit, float.MaxValue, LayerMask.GetMask("Terrain"))){
            upTree.SetPositionAndRotation(hit.point, Quaternion.LookRotation(Vector3.forward, hit.normal));
        }
        if(downTree && Physics.Raycast(castPosition + Vector3.down * 100, Vector3.up, out hit, float.MaxValue, LayerMask.GetMask("Terrain"))){
            downTree.SetPositionAndRotation(hit.point, Quaternion.LookRotation(Vector3.forward, hit.normal));
        }

    }
    
}
