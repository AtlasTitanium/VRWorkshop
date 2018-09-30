using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sonar : MonoBehaviour {
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private bool gripButtonDown = false;
	private bool gripButtonUp = false;
	private bool gripButtonPressed = false;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private bool TriggerButtonDown = false;
	private bool TriggerButtonUp = false;
	private bool TriggerButtonPressed = false;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller {get {return SteamVR_Controller.Input((int) trackedObj.index);}}
	public Camera YourHead;
	private FixedJoint ControllerJoint;
	private FixedJoint OtherControllerJoint;

	public int maxLength = 500;
	private int counter;
	public int lenght;

	private int fixedLenth;

	private bool goSonic = false;
	private bool goTails = false;

	public float seconds = 1.0f;
	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		ControllerJoint = GetComponent<FixedJoint>();
		YourHead.farClipPlane = lenght;
	}
	
	// Update is called once per frame
	void Update () {
		if(controller == null){
			Debug.Log("No controllers Bro");
			return;
		} 

		if(goSonic){
			YourHead.nearClipPlane = counter;
			YourHead.farClipPlane = fixedLenth;
			counter += 1;
			fixedLenth += 1;
			StartCoroutine(WaitATic());
			if(counter >= maxLength){
				YourHead.nearClipPlane = 0.05f;
				YourHead.farClipPlane = lenght;
				goSonic = false;
			}
		}

		if(goTails){
			YourHead.nearClipPlane = fixedLenth;
			YourHead.farClipPlane = counter;
			counter -= 1;
			fixedLenth -= 1;
			if(counter <= 0){
				YourHead.nearClipPlane = 0.05f;
				YourHead.farClipPlane = lenght;
				goTails = false;
			}
		}

		gripButtonDown = controller.GetPressDown(gripButton);
		gripButtonUp = controller.GetPressUp(gripButton);
		gripButtonPressed = controller.GetPress(gripButton);

		TriggerButtonDown = controller.GetPressDown(triggerButton);
		TriggerButtonUp = controller.GetPressUp(triggerButton);
		TriggerButtonPressed = controller.GetPress(triggerButton);

		if(TriggerButtonDown){
			Sonario();
			Debug.Log("Trying to tase me bro?");
		}
		if(gripButtonDown){
			ReverseSonar();
			Debug.Log("Grippin on tight bro?");
		}
	}


	public void Sonario(){
		goSonic = false;
		counter = 0;
		fixedLenth = lenght;
		goSonic = true;
	}

	public void ReverseSonar(){
		goTails = false;
		counter = maxLength;
		fixedLenth = maxLength - lenght;
		goTails = true;
	}

	IEnumerator WaitATic()
    {
		goSonic = false;
        yield return new WaitForSeconds(seconds);
		goSonic = true;
    }
}

