using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float rotationSpeed = 1;
    public Transform target, player;
    float mouseX, mouseY;
    float mouseYClamp = 85f;

    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update() {
        CameraManager();
    }

    void CameraManager() {
        mouseX += Input.GetAxis("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxis("Mouse Y") * rotationSpeed;
        mouseY = Mathf.Clamp(mouseY, -mouseYClamp, mouseYClamp);
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0f);
        transform.LookAt(player);
    }
}
