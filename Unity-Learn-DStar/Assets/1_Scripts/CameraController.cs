using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float MaxZoom;
    public float MinZoom;
    public float Sensitivity;

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
        InputManager.Instance.OnScroll += OnScroll;
    }

    private void OnMouseMove(Vector3 _mousePos, Vector3 _previousMousePos)
    {
        if (rightMouseDown)
        {
            cam.transform.position -= (_mousePos - _previousMousePos) * Sensitivity;
        }
    }

    private void OnScroll(Vector2 _scrollDelta)
    {
        if (_scrollDelta.y < 0)
        {
            if (cam.orthographicSize > MinZoom)
            {
                cam.orthographicSize -= _scrollDelta.y;
            }
            
            if (cam.orthographicSize > MaxZoom)
            {
                cam.orthographicSize = MaxZoom - 0.01f;
            }
        }
        else if (_scrollDelta.y > 0)
        {
            if (cam.orthographicSize < MaxZoom)
            {
                cam.orthographicSize -= _scrollDelta.y;
            }

            if (cam.orthographicSize < MinZoom)
            {
                cam.orthographicSize = MinZoom + 0.01f;
            }
        }
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
