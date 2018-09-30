using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickStuffUp : MonoBehaviour {
	public GameObject ControllerParticle;
	public GameObject GreenLight;
	public GameObject RedLight;
	public GameObject InsideLight;
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private bool TriggerButtonDown = false;
	private bool TriggerButtonUp = false;
	private bool TriggerButtonPressed = false;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_Controller.Device controller {get {return SteamVR_Controller.Input((int) trackedObj.index);}}


	public GameObject Object;
	public FixedJoint ControllerJoint;
	private bool Throwing;
	private Rigidbody RBObject;
	// Use this for initialization
	void Start () {
		trackedObj = GetComponent<SteamVR_TrackedObject>();
		ControllerJoint = GetComponent<FixedJoint>();
		GreenLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.gray);
		RedLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.red);
		InsideLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.red);
	}
	
	// Update is called once per frame
	void Update () {
		if(controller == null){
			Debug.Log("No controllers Bro");
			return;
		} 

		TriggerButtonDown = controller.GetPressDown(triggerButton);
		TriggerButtonUp = controller.GetPressUp(triggerButton);
		TriggerButtonPressed = controller.GetPress(triggerButton);

		if(TriggerButtonDown){
			PickUpObject();
			Debug.Log("Trying to tase me bro?");
		}
		if(TriggerButtonUp){
			DropObject();
			Debug.Log("I have the higher ground now Anakin");
		}

		if(controller.GetPressDown(gripButton)){
			Application.LoadLevel(Application.loadedLevel);
		}
	}

	void FixedUpdate(){
		if(Throwing){
			Transform origin;
			if(trackedObj.origin != null){
				origin = trackedObj.origin;
			} else {
				origin = trackedObj.transform.parent;
			}
			if(origin != null){
				RBObject.velocity = origin.TransformVector(controller.velocity);
				RBObject.angularVelocity = origin.TransformVector(controller.angularVelocity);
			} else{
				RBObject.velocity = controller.velocity;
				RBObject.velocity = controller.angularVelocity;
			}

			RBObject.maxAngularVelocity = RBObject.angularVelocity.magnitude;

			Throwing = false;
		}
	}

	void OnTriggerStay(Collider other){
		if(other.tag == "Pickup"){
			GreenLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.green);
			Object = other.gameObject;
			Object.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.05f);
			//Object.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.yellow);
		}
	}

	void OnTriggerExit(Collider other){
		//Object.GetComponent<Renderer>().material.SetColor("_OutlineColor", Color.black);
		if(Object != null){
			GreenLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.gray);
			Object.GetComponent<Renderer>().material.SetFloat("_OutlineWidth", 1.00f);
			Object = null;
		}
	}

	public void PickUpObject(){
		if(Object != null){
			if(ControllerParticle != null){
				ControllerParticle.SetActive(false);
			}
			GreenLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.green);
			RedLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.gray);
			InsideLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.green);
			ControllerJoint.connectedBody = Object.GetComponent<Rigidbody>();
			Object.GetComponent<Collider>().isTrigger = true;
			

			Throwing = false;
			RBObject = null;
		} else {
			ControllerJoint.connectedBody = null;
		}
	}

	public void DropObject(){
		if(ControllerJoint.connectedBody != null){
			if(ControllerParticle != null){
				ControllerParticle.SetActive(true);
			}
			GreenLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.gray);
			RedLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.red);
			InsideLight.GetComponent<Renderer>().material.SetColor ("_EmissionColor", Color.red);
			RBObject = ControllerJoint.connectedBody;
			RBObject.gameObject.GetComponent<Collider>().isTrigger = false;
			

			ControllerJoint.connectedBody = null;

			Throwing = true;
		}
	}
}
