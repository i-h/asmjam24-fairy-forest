using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherSideManager : MonoBehaviour
{
    public enum World {None, Normal, OtherSide}
    public static event System.Action<World> WorldChanged;
    private World _currentWorld = World.Normal;
    private Transform _player;
    private void Start(){
        _player = GameObject.FindWithTag("Player").transform;

    }
    private void Update()
    {
        if(_currentWorld == World.Normal && _player.position.y < 0){
            ChangeWorld(World.OtherSide);
        } else if(_currentWorld == World.OtherSide && _player.position.y > 0){
            ChangeWorld(World.Normal);
        }
    }
    private void ChangeWorld(World world){
        Debug.Log($"World changed: {world}");
        _currentWorld = world;
        WorldChanged?.Invoke(world);
    }
}
