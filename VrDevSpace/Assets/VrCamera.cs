using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrCamera : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private bool gripButtonDown = false;
	private bool gripButtonUp = false;
	private bool gripButtonPressed = false;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private bool TriggerButtonDown = false;
	private bool TriggerButtonUp = false;
	private bool TriggerButtonPressed = false;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_TrackedObject OthertrackedObj;
	private SteamVR_Controller.Device controller {get {return SteamVR_Controller.Input((int) trackedObj.index);}}
	private SteamVR_Controller.Device Othercontroller {get {return SteamVR_Controller.Input((int) OthertrackedObj.index);}}

	public GameObject OtherController;
	public Camera Camera;
	private FixedJoint ControllerJoint;
	private FixedJoint OtherControllerJoint;
	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		OthertrackedObj = OtherController.GetComponent<SteamVR_TrackedObject>();
		ControllerJoint = GetComponent<FixedJoint>();
		OtherControllerJoint = OtherController.GetComponent<FixedJoint>();
	}
	
	// Update is called once per frame
	void Update () {
		if(controller == null){
			Debug.Log("No controllers Bro");
			return;
		} 

		gripButtonDown = controller.GetPressDown(gripButton);
		gripButtonUp = controller.GetPressUp(gripButton);
		gripButtonPressed = controller.GetPress(gripButton);

		TriggerButtonDown = controller.GetPressDown(triggerButton);
		TriggerButtonUp = controller.GetPressUp(triggerButton);
		TriggerButtonPressed = controller.GetPress(triggerButton);

		if(TriggerButtonDown){
			TakePicture();
			Debug.Log("Trying to tase me bro?");
		}
		if(gripButtonDown){
			ResetCamera();
			Debug.Log("Grippin on tight bro?");
		}
	}


	public void TakePicture(){
		Camera.gameObject.SetActive(false);
	}

	public void ResetCamera(){
		Camera.gameObject.SetActive(true);
	}
}
