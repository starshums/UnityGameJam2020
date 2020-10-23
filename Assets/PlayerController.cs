using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private CharacterController controller = null;
    public Vector3 moveDirection;
    public float moveSpeed = 10f;
    public float gravityScale = 5f;
    public float jumpForce = 25f;

    void Start() {
        controller = GetComponent<CharacterController> ();
    }

    void Update() {
        Move();
    }

    private void Move () {
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
		moveDirection = moveDirection.normalized * moveSpeed;
		moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
		controller.Move( moveDirection * Time.deltaTime );
	}

}
