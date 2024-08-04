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
    public static void WinGame(){
        Debug.Log("voitit pelin btw");
    }
    [SerializeField] private ColorCurves _curves; 
    [SerializeField] private float _normalGravity = -20f;
    [SerializeField] private float _otherSideGravity = 10;
    [SerializeField] private Color _otherSideColor = Color.cyan;


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
        var otherSide = world == OtherSideManager.World.OtherSide;
        RenderSettings.fog = otherSide;
        Camera.main.clearFlags = otherSide ? CameraClearFlags.SolidColor : CameraClearFlags.Skybox;

        switch(world){
            case OtherSideManager.World.Normal:
                Physics.gravity = Vector3.up * _normalGravity;
            break;
            case OtherSideManager.World.OtherSide:
                Physics.gravity = Vector3.up * _otherSideGravity;
                Camera.main.backgroundColor = _otherSideColor;
                RenderSettings.fogColor = _otherSideColor;
            break;
        }
    }
    private void Update(){
        if(Input.GetKeyDown(KeyCode.F11)){
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
