using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJump : MonoBehaviour {

	public SteamVR_TrackedObject left;
	public SteamVR_TrackedObject right;
    public SteamVR_TrackedObject head;
	public float jump_threshold = 2f;
	public float jump_strength = 5f;
    public float horizontal_strength = 25f;

	private ulong trigger = SteamVR_Controller.ButtonMask.Trigger;
	private Rigidbody body;
    private SteamVR_Controller.Device hmd;
    private float height;


	// Use this for initialization
	void Start () {
		body = GetComponent<Rigidbody> ();
        hmd = SteamVR_Controller.Input((int)head.index);
        height = hmd.transform.pos.y;
        print(height);
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
			Vector3 v = hmd.velocity;
            //print(hmd.transform.pos.y + " " + height + " " + v.magnitude + " " + v.y);
			if (v.magnitude > jump_threshold && v.y > 0 && hmd.transform.pos.y > height) {
                print(v);
                // If so, perform a jump
                // Jump ();
                // body.velocity = jump_strength * v.y * Vector3.up;
                Vector3 tmp = new Vector3(horizontal_strength * v.x, jump_strength * v.y, horizontal_strength * v.z);
                if (tmp.sqrMagnitude > body.velocity.sqrMagnitude)
                {
                    body.velocity = tmp;
                }
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
