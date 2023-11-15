using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera cam;
    private bool rightMouseDown;

    public void Awake()
    {
        cam = GetComponent<Camera>();
        if (cam == null) { Debug.LogError("CameraController couldn't find Camera"); }
    }

    public void Start()
    {
        InputManager.Instance.OnRightMouseButtonDown += OnRightMouseButtonDown;
        InputManager.Instance.OnRightMouseButtonUp += OnRightMouseButtonUp;
        InputManager.Instance.OnMouseMove += OnMouseMove;
    }

    public void Update()
    {
        
    }

    private void OnMouseMove(Vector3 _mousePos)
    {

    }

    private void OnRightMouseButtonDown()
    {
        rightMouseDown = true;
    }

    private void OnRightMouseButtonUp()
    {
        rightMouseDown = false;
    }
}
