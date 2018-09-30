using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	[Range (0f,2.5f)]
	public float movementSpeed = 1.5f;
	public GameObject Head;

	public SteamVR_TrackedObject Left_trackedObj;
	public SteamVR_TrackedObject Right_trackedObj;

	void Update () {
		var L_device = SteamVR_Controller.Input((int)Left_trackedObj.index);
		var R_device = SteamVR_Controller.Input((int)Right_trackedObj.index);
		if((L_device.velocity.y > movementSpeed && R_device.velocity.y < -movementSpeed) || (L_device.velocity.y < -movementSpeed && R_device.velocity.y > movementSpeed)){
			GetComponent<Rigidbody>().AddForce(Head.transform.forward * 10);
		}
	}
}

