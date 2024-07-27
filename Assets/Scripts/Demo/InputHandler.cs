using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Variable to handle Input
    private Vector2 moveDirection;
    private Vector2 mousePosition;
    private Action onBuildCallBack;
    private Action onSelectCallBack; 
    public Vector2 MoveDirection => moveDirection.normalized;

    public Vector2 MousePosition => mousePosition;


    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    private void MoveInput(Vector2 input)
    {
        moveDirection = input;
    }

    public void OnMouseMove(InputValue value)
    {
        MouseInput(value.Get<Vector2>());
    }

    private void MouseInput(Vector2 input)
    {
        mousePosition = input;
    }

    public void OnUseAbility(InputValue value)
    {
        if (value.isPressed)
        {
            onBuildCallBack?.Invoke();
        }
    }

    public void OnSelect(InputValue value)
    {
        if(value.isPressed)
        {
            onSelectCallBack?.Invoke();
        }
    }

    public void AssignSelectCallBack(Action callback)
    {
        onSelectCallBack = callback;
    }

    public void AssignBuildCallBack(Action callback)
    {
        onBuildCallBack = callback;
    }
}
