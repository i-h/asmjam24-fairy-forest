using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool GameStarted = false;
    public bool Paused = false;
    public static void StartGame() =>    instance.GameStarted = true;
    public static void Pause(bool pause) => instance.Paused = pause;
    [SerializeField] private ColorCurves _curves; 
    [SerializeField] private float _normalGravity = -20f;
    [SerializeField] private float _otherSideGravity = 10;


    private void OnValidate(){

    }
    private void Awake(){
        instance = this;
    }
    private void Start(){
        OtherSideManager.WorldChanged += OnWorldChanged;
    }
    private void OnDestroy(){
        OtherSideManager.WorldChanged -= OnWorldChanged;
    }

    private void OnWorldChanged(OtherSideManager.World world){
        RenderSettings.fog = world == OtherSideManager.World.OtherSide;
        switch(world){
            case OtherSideManager.World.Normal:
                Physics.gravity = Vector3.up * _normalGravity;
            break;
            case OtherSideManager.World.OtherSide:
                Physics.gravity = Vector3.up * _otherSideGravity;
            break;
        }
    }
}
