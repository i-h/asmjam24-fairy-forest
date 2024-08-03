using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSideManager : MonoBehaviour
{
    public enum World {None, Normal, OtherSide}
    public static event System.Action<World> WorldChanged;
    private World _currentWorld = World.None;
    private Transform _player;

    /// <summary>
    public SmoothRotate smoothRotate;
    /// </summary>
    private void Start(){
        _player = GameObject.FindWithTag("Player").transform;
        smoothRotate = _player.GetComponent<SmoothRotate>();

    }
    private void Update()
    {
        if(_currentWorld != World.OtherSide && _player.position.y < 0){
          ChangeWorld(World.OtherSide);
          smoothRotate.RotateLeft();
        } else if(_currentWorld != World.Normal && _player.position.y > 0){
          ChangeWorld(World.Normal);
          smoothRotate.RotateRight();
        }
    }
    private void ChangeWorld(World world){
        Debug.Log($"World changed: {world}");
        _currentWorld = world;
        WorldChanged?.Invoke(world);
    }
}
