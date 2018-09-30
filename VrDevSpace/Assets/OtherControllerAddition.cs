using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherControllerAddition : MonoBehaviour {
	public WandController WandControllerScript;

	void OnTriggerStay(Collider other){
		if(other.tag == "Pickup"){
			WandControllerScript.Object = other.gameObject;
		}
	}

	void OnTriggerExit(Collider other){
		WandControllerScript.Object = null;
	}
}
