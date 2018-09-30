using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {
	public UI_Manager UIManager;
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			UIManager.GameOver();
			UIManager.Die();
		}
	}
}
