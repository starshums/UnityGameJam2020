using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Animator animator = null;
    private CharacterController controller = null;
    public Vector3 moveDirection;
    private float moveSpeed = 10f;
    public float gravityScale = 100f;
    public float jumpForce = 400f;
    public Transform target;
    public GameObject playerModel;

    private float rotateSpeed = 0.3f;
    public HealthManager healthManager;
    public int maxHealth = 200;
    public int currentHealth;

    void Start() {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthManager.SetMaxHealth(maxHealth);
    }

    void Update() {
        Move();
    }

    private void Move () {
        moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
		moveDirection = moveDirection.normalized * moveSpeed;
        // Jumping
        if( controller.isGrounded && Input.GetButtonDown("Jump") ) StartCoroutine("Jumping");
		moveDirection.y += Physics.gravity.y * gravityScale * Time.deltaTime;
		controller.Move( moveDirection * Time.deltaTime );
        // Move player in different directions
		if (Input.GetAxis ("Vertical") != 0 || Input.GetAxis ("Horizontal") != 0) {
			transform.rotation = Quaternion.Euler(0f, target.rotation.eulerAngles.y, 0f);
			Quaternion rotatePlayer = Quaternion.LookRotation (new Vector3 (moveDirection.x, 0f, moveDirection.z));
			playerModel.transform.rotation = Quaternion.Slerp(playerModel.transform.rotation, rotatePlayer, rotateSpeed);
		}

        // Running & Idle Animations
        animator.SetFloat("Speed", (Mathf.Abs(Input.GetAxis ("Vertical")) + Mathf.Abs(Input.GetAxis ("Horizontal"))));
	}

    IEnumerator Jumping() {
        moveDirection.y = jumpForce;
        yield return new WaitForSeconds(1);
    }

    public void Damage(int damage) {
        currentHealth -= damage;
        healthManager.SetHealth(currentHealth);
    }

}