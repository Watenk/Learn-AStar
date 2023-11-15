using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    //Mouse
    public event Action OnLeftMouseButton;
    public event Action OnRightMouseButtonDown;
    public event Action OnRightMouseButtonUp;
    public event Action OnMiddleMouseButton;
    public event Action<Vector3> OnMouseMove;

    private Vector3 previousMousePos;

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
        if (Input.GetMouseButton(0) && OnLeftMouseButton != null) { OnLeftMouseButton(); }
        if (Input.GetMouseButtonDown(1) && OnRightMouseButtonDown != null) { OnRightMouseButtonDown(); }
        if (Input.GetMouseButtonUp(1) && OnRightMouseButtonUp != null) { OnRightMouseButtonUp(); }
        if (Input.GetMouseButton(2) && OnMiddleMouseButton != null) { OnMiddleMouseButton(); }

        //Pos
        if (Input.mousePosition != previousMousePos && OnMouseMove != null)
        {
            OnMouseMove(Input.mousePosition);
        }
        previousMousePos = Input.mousePosition;
    }
}