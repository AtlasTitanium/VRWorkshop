using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SteamVrRecenter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Input.GetKeyUp("l")){
			var system = OpenVR.System;
			if(null != system){
				system.ResetSeatedZeroPose();
			}
		}
	}
}
