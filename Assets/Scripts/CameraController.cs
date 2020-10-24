using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float rotationSpeed = 1;
    public Transform target, player;
    float mouseX, mouseY;
    float mouseYClamp = 85f;

    public float targetZoom = 30.0f, zoom = 30.0f;
    private float zoomSpeed = 5.0f;
    private Vector2 zoomClamp = new Vector2(0.5f, 30.0f);
    private float zoomVelocity = 0.0f;
    private float zoomDamp = 0.05f;
    private float cameraRadius = 1f;
    public Vector3 defaultPosition = new Vector3(0, 3, -20);
    private float journeyTime = 1.0f;
    private float startTime;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        CameraManager();
        CameraCollisions();
    }

    void CameraManager() {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -mouseYClamp, mouseYClamp);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        transform.LookAt(player);
    }

    void CameraCollisions() {
        targetZoom += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        targetZoom = Mathf.Clamp(targetZoom, zoomClamp.x, zoomClamp.y);

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        RaycastHit tempHit;
        if(Physics.Raycast(target.position, -target.forward, out tempHit, targetZoom, layerMask)) {
            zoom = Mathf.SmoothDamp(zoom, tempHit.distance, ref zoomVelocity, zoomDamp);
            zoom -= cameraRadius;
        } else {
            zoom = Mathf.SmoothDamp(zoom, targetZoom, ref zoomVelocity, zoomDamp);
        }

        if(zoom <= 5) zoom = 5;

        zoom = Mathf.Clamp(zoom, zoomClamp.x, zoomClamp.y);
        Vector3 pos = Vector3.back * zoom;
        float fracComplete = (Time.time - startTime) / journeyTime;
        transform.localPosition = Vector3.Slerp(transform.localPosition, pos, fracComplete);
    }

}