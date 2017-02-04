using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour {

	public SteamVR_TrackedObject left;
	public SteamVR_TrackedObject right;
	public float jump_threshold = 2f;
	public float jump_strength = 5f;

	private ulong trigger = SteamVR_Controller.ButtonMask.Trigger;
	private Rigidbody body;

	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
	}

	// Called every fixed period
	void FixedUpdate () {
		// Set triggerPressed
		bool triggerPressed = false;
		if (left.isActiveAndEnabled) {
			SteamVR_Controller.Device leftDevice = SteamVR_Controller.Input ((int)left.index);
			if (leftDevice.GetPress (trigger)) {
				triggerPressed = true;
			}
		}
		if (right.isActiveAndEnabled) {
			SteamVR_Controller.Device rightDevice = SteamVR_Controller.Input ((int)right.index);
			if (rightDevice.GetPress (trigger)) {
				triggerPressed = true;
			}
		}

		// Does the jump if appropriate
		if (triggerPressed) {
			// Check if our upwards velocity is above a certain threshold
			Vector3 v = body.velocity;
			if (Vector3.Dot (v, Vector3.up) > jump_threshold) {
				// If so, perform a jump
				Jump ();
			}
		}
	}

	// Called every frame
	void Update () {
		// Test code to manually jump by hitting space
		if (Input.GetKeyDown (KeyCode.Space)) {
			print ("Spacebar pressed.");
			Jump ();
		}
	}

	// Causes the player / player space to launch upwards
	void Jump() {
		print ("Jump function called.");
		body.AddForce (jump_strength * Vector3.up); // Going straight up
		// body.AddForce(jump_strength * Vector3.up); // Following direction of trigger velocity
	}
}
