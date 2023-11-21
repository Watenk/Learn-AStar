using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    //Mouse
    public event Action OnLeftMouseButtonDown;
    public event Action OnRightMouseButtonDown;
    public event Action OnRightMouseButtonUp;
    public event Action OnMiddleMouseButton;
    public event Action<Vector3, Vector3> OnMouseMove;
    public event Action<Vector2> OnScroll;

    private Vector3 previousMousePos;
    private Vector2 previousScrollDelta;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        UpdateMouse();
    }

    private void UpdateMouse()
    {
        //Buttons
        if (Input.GetMouseButtonDown(0) && OnLeftMouseButtonDown != null) { OnLeftMouseButtonDown(); }
        if (Input.GetMouseButtonDown(1) && OnRightMouseButtonDown != null) { OnRightMouseButtonDown(); }
        if (Input.GetMouseButtonUp(1) && OnRightMouseButtonUp != null) { OnRightMouseButtonUp(); }
        if (Input.GetMouseButton(2) && OnMiddleMouseButton != null) { OnMiddleMouseButton(); }

        //Pos
        if (Input.mousePosition != previousMousePos && OnMouseMove != null)
        {
            OnMouseMove(Input.mousePosition, previousMousePos);
        }
        previousMousePos = Input.mousePosition;

        if (Input.mouseScrollDelta != previousScrollDelta && OnScroll != null)
        {
            OnScroll(Input.mouseScrollDelta);
        }
        previousScrollDelta = Input.mouseScrollDelta;
    }
}
