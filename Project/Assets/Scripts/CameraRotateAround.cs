using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraRotateAround : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform target;           
    public Vector3 offset = new Vector3(0, 2, -5); 

    [Header("Rotation Settings")]
    public float rotationSpeed = 0.5f;
    public float yAngleLimit = 80f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 0.07f;
    public float minZoom = 3f;      
    public float maxZoom = 10f; 
    private float currentZoom;
    private Vector2 currentRotation;
    private bool isResetting;
    private float initialTouchDistance;
    void Start()
    {
        if (target == null)
        {
            Debug.LogError("Target not assigned! Please assign a target in the inspector.");
            return;
        }
        currentZoom = Mathf.Clamp(-offset.z, minZoom, maxZoom);
        currentRotation = Vector2.zero;
        UpdateCameraPosition(true);
    }
    void Update()
    {
        if (target == null || isResetting) return;
        if (Input.touchCount == 1 || Input.GetMouseButton(0))
        {
            Vector2 inputDelta = GetInputDelta();
            currentRotation.x += inputDelta.x * rotationSpeed;
            currentRotation.y -= inputDelta.y * rotationSpeed;
            currentRotation.y = Mathf.Clamp(currentRotation.y, -yAngleLimit, yAngleLimit);
            UpdateCameraPosition();
        }
        if (Input.touchCount == 2)
        {
            HandlePinchZoom();
        }
        else if (Input.mouseScrollDelta.y != 0)
        {
            currentZoom = Mathf.Clamp(currentZoom - Input.mouseScrollDelta.y * zoomSpeed * 5, minZoom, maxZoom);
            UpdateCameraPosition();
        }
        if ((Input.touchCount == 1 && Input.GetTouch(0).tapCount == 2) || Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(ResetCamera());
        }
    }
    private Vector2 GetInputDelta()
    {
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            return touch.deltaPosition;
        }
        else if (Input.GetMouseButton(0))
        {
            return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * 10f;
        }
        return Vector2.zero;
    }
    private void HandlePinchZoom()
    {
        Touch touch1 = Input.GetTouch(0);
        Touch touch2 = Input.GetTouch(1);

        if (touch2.phase == TouchPhase.Began)
        {
            initialTouchDistance = Vector2.Distance(touch1.position, touch2.position);
        }
        else if (touch1.phase == TouchPhase.Moved || touch2.phase == TouchPhase.Moved)
        {
            float currentTouchDistance = Vector2.Distance(touch1.position, touch2.position);
            float delta = (initialTouchDistance - currentTouchDistance) * zoomSpeed;
            currentZoom = Mathf.Clamp(currentZoom + delta, minZoom, maxZoom);
            initialTouchDistance = currentTouchDistance;
            UpdateCameraPosition();
        }
    }
    private void UpdateCameraPosition(bool forceUpdate = false)
    {
        if (forceUpdate || !isResetting)
        {
            Quaternion rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            Vector3 newPosition = target.position + rotation * new Vector3(0, 0, -currentZoom);
            transform.position = newPosition;
            transform.LookAt(target);
        }
    }
    private IEnumerator ResetCamera()
    {
        isResetting = true;
        float duration = 0.5f;
        float elapsed = 0;

        Vector2 startRotation = currentRotation;
        float startZoom = currentZoom;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            currentRotation = Vector2.Lerp(startRotation, Vector2.zero, t);
            currentZoom = Mathf.Lerp(startZoom, Mathf.Clamp(-offset.z, minZoom, maxZoom), t);
            UpdateCameraPosition();

            yield return null;
        }
        currentRotation = Vector2.zero;
        currentZoom = Mathf.Clamp(-offset.z, minZoom, maxZoom);
        UpdateCameraPosition(true);
        isResetting = false;
    }
}