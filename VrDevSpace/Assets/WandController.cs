using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WandController : MonoBehaviour {
	public GameObject HandPrefab;
	public GameObject HeadPrefab;
	private GameObject ThisHand;
	private GameObject OtherHand;
	private GameObject MadeHead;
	public GameObject HeadGear;
	public GameObject OtherController;
	private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
	private bool gripButtonDown = false;
	private bool gripButtonUp = false;
	private bool gripButtonPressed = false;
	private bool OthergripButtonDown = false;
	private bool OthergripButtonUp = false;
	private bool OthergripButtonPressed = false;

	private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
	private bool TriggerButtonDown = false;
	private bool TriggerButtonUp = false;
	private bool TriggerButtonPressed = false;
	private bool OtherTriggerButtonDown = false;
	private bool OtherTriggerButtonUp = false;
	private bool OtherTriggerButtonPressed = false;
	private SteamVR_TrackedObject trackedObj;
	private SteamVR_TrackedObject OthertrackedObj;
	private SteamVR_Controller.Device controller {get {return SteamVR_Controller.Input((int) trackedObj.index);}}
	private SteamVR_Controller.Device Othercontroller {get {return SteamVR_Controller.Input((int) OthertrackedObj.index);}}


	public GameObject Object;
	public FixedJoint ControllerJoint;
	public FixedJoint OtherControllerJoint;
	private bool Throwing;
	private bool OtherThrowing;
	private Rigidbody RBObject;
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

		OthergripButtonDown = Othercontroller.GetPressDown(gripButton);
		OthergripButtonUp = Othercontroller.GetPressUp(gripButton);
		OthergripButtonPressed = Othercontroller.GetPress(gripButton);

		OtherTriggerButtonDown = Othercontroller.GetPressDown(triggerButton);
		OtherTriggerButtonUp = Othercontroller.GetPressUp(triggerButton);
		OtherTriggerButtonPressed = Othercontroller.GetPress(triggerButton);

		if(gripButtonDown || OthergripButtonDown){
			LockPuppet();
			Debug.Log("Grippin on tight bro?");
		}
		if(gripButtonUp || OthergripButtonUp){
			Debug.Log("Ahhhh don't let go");
		}
		if(OtherTriggerButtonDown){
			OtherPickUpObject();
			Debug.Log("Trying to tase me bro?");
		}
		if(TriggerButtonDown){
			PickUpObject();
			Debug.Log("Trying to tase me bro?");
		}
		if(OtherTriggerButtonUp){
			OtherDropObject();
			Debug.Log("I have the higher ground now Anakin");
		}
		if(TriggerButtonUp){
			DropObject();
			Debug.Log("I have the higher ground now Anakin");
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
		if(OtherThrowing){
			Transform origin;
			if(trackedObj.origin != null){
				origin = trackedObj.origin;
			} else {
				origin = trackedObj.transform.parent;
			}
			if(origin != null){
				RBObject.velocity = origin.TransformVector(Othercontroller.velocity);
				RBObject.angularVelocity = origin.TransformVector(Othercontroller.angularVelocity);
			} else{
				RBObject.velocity = Othercontroller.velocity;
				RBObject.velocity = Othercontroller.angularVelocity;
			}

			RBObject.maxAngularVelocity = RBObject.angularVelocity.magnitude;

			OtherThrowing = false;
		}
	}

	void OnTriggerStay(Collider other){
		if(other.tag == "Pickup"){
			Object = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other){
		Object = null;
	}

	public void PickUpObject(){
		if(Object != null){
			ControllerJoint.connectedBody = Object.GetComponent<Rigidbody>();

			Throwing = false;
			RBObject = null;
		} else {
			ControllerJoint.connectedBody = null;
		}
	}

	public void DropObject(){
		if(ControllerJoint.connectedBody != null){
			RBObject = ControllerJoint.connectedBody;

			ControllerJoint.connectedBody = null;

			Throwing = true;
		}
	}

	public void OtherPickUpObject(){
		if(Object != null){
			OtherControllerJoint.connectedBody = Object.GetComponent<Rigidbody>();

			OtherThrowing = false;
			RBObject = null;
		} else {
			OtherControllerJoint.connectedBody = null;
		}
	}

	public void OtherDropObject(){
		if(OtherControllerJoint.connectedBody != null){
			RBObject = OtherControllerJoint.connectedBody;

			OtherControllerJoint.connectedBody = null;

			OtherThrowing = true;
		}
	}

	public void LockPuppet(){
		if(ThisHand != null){Destroy(ThisHand);}
		ThisHand = Instantiate(HandPrefab, ControllerJoint.gameObject.transform.position, ControllerJoint.gameObject.transform.rotation);
		if(OtherHand != null){Destroy(OtherHand);}
		OtherHand = Instantiate(HandPrefab, OtherControllerJoint.gameObject.transform.position, OtherControllerJoint.gameObject.transform.rotation);
		if(MadeHead != null){Destroy(MadeHead);}
		MadeHead = Instantiate(HeadPrefab, HeadGear.transform.position, HeadGear.transform.rotation);
	}
}
