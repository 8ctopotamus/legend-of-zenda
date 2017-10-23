using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[Header("Visuals")]
	public GameObject model;
	public float rotatingSpeed = 2f;

	[Header("Movement")]
	public float movingVelocity;
	public float jumpingVelocity = 200f;

	private Rigidbody playerRigidBody;
	private bool canJump;
	private Quaternion targetModelRotation;

	void Start () {
		playerRigidBody = GetComponent<Rigidbody>();
		targetModelRotation = Quaternion.Euler(0, 0, 0);
	}

	void Update () {
		// Raycast to identify if the player can jump.
		RaycastHit hit;
		if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.01f)) {
			canJump = true;
		}

		model.transform.rotation = Quaternion.Lerp(
			model.transform.rotation,
			targetModelRotation,
			Time.deltaTime * rotatingSpeed
		);

		ProcessInput();
	}

	void ProcessInput () {
		// slow down if nothing pressed
		playerRigidBody.velocity = new Vector3(
			0,
			playerRigidBody.velocity.y,
			0
		);

		// Move in the XZ plane
		if (Input.GetKey("right")) {
			playerRigidBody.velocity = new Vector3(
				movingVelocity,
				playerRigidBody.velocity.y,
				playerRigidBody.velocity.z
			);

			targetModelRotation = Quaternion.Euler(0, 270, 0);
		}
		if (Input.GetKey("left")) {
			playerRigidBody.velocity = new Vector3(
				-movingVelocity,
				playerRigidBody.velocity.y,
				playerRigidBody.velocity.z
			);

			targetModelRotation = Quaternion.Euler(0, 90, 0);
		}
		if (Input.GetKey("up")) {
			playerRigidBody.velocity = new Vector3(
				playerRigidBody.velocity.x,
				playerRigidBody.velocity.y,
				movingVelocity
			);

			targetModelRotation = Quaternion.Euler(0, 180, 0);
		}
		if (Input.GetKey("down")) {
			playerRigidBody.velocity = new Vector3(
				playerRigidBody.velocity.x,
				playerRigidBody.velocity.y,
				-movingVelocity
			);

			targetModelRotation = Quaternion.Euler(0, 0, 0);
		}

		// Check for jumps.
		if (canJump && Input.GetKeyDown("space")) {
			playerRigidBody.velocity = new Vector3(
				playerRigidBody.velocity.x,
				jumpingVelocity,
				playerRigidBody.velocity.z
			);
		}
	}

}
