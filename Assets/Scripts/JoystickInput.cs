using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputHandler : MonoBehaviour {
    public abstract Vector2 GetInput();
    public abstract bool GetJump();
}

public class JoystickInput : InputHandler
{
    private Vector2 _input = new Vector2();
    public override Vector2 GetInput() => _input;
    public override bool GetJump() => Input.GetButtonDown("Jump");

    void Update(){
        _input.x = Input.GetAxis("Horizontal");
        _input.y = Input.GetAxis("Vertical");
    }
}
