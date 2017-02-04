using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerTest : MonoBehaviour {

	SteamVR_TrackedObject trackedObj;

	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		SteamVR_Controller.Device dev = SteamVR_Controller.Input ((int) trackedObj.index);
		print ((int)trackedObj.index);
	}
}
