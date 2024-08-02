using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatialField : MonoBehaviour
{
    [SerializeField] private float scale = Mathf.PI * 2;
    public float GetValue(float x, float y, float z){
        return (Evaluate(x)+Evaluate(y)+Evaluate(z))/3f;
    }
    private float Evaluate(float val) 
        => Mathf.Pow(((-Mathf.Cos(2 * Mathf.PI * (val / scale)) + 1) / 2), Mathf.Exp(1));

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmosSelected(){
        const int area = 6;
        const float gap = 3f;
        for(int i = area; i > -area; i--){
            for(int j = area; j > -area; j--){
                for(int k = -area; k < area; k++){
                    float x = transform.position.x + i * gap;
                    float y = transform.position.y + k * gap;
                    float z = transform.position.z + j * gap;
                    var val = GetValue(x,y,z);
                    Gizmos.DrawSphere(new Vector3(x,y,z), val*gap);
                }
            }
        }

    }
}
