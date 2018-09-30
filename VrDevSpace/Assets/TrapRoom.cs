using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapRoom : MonoBehaviour {

	public GameObject TrapWalls;
	public UI_Manager UIManager;
	public GameObject Lasers;
	public Transform LaserBarrier1, LaserBarrier2;
	public int speed = 1;
	private bool LasersOn = false;
	private bool moving = true;
	private bool no = false;
	private bool yes = false;
	void Update(){
		if(LasersOn){
			if(Lasers.transform.position.x > LaserBarrier1.position.x){
				Debug.Log("Moving");
				moving = true;
			}
			if(Lasers.transform.position.x < LaserBarrier2.position.x){
				Debug.Log("Moving back");
				moving = false;
			}

			if(moving){
				Lasers.transform.position = new Vector3(Lasers.transform.position.x - speed*Time.deltaTime,Lasers.transform.position.y, Lasers.transform.position.z);
			} else {
				Lasers.transform.position = new Vector3(Lasers.transform.position.x + speed*Time.deltaTime,Lasers.transform.position.y, Lasers.transform.position.z);
			}
			//Lasers.transform.position = new Vector3(Mathf.Lerp(Lasers.transform.position.x, Lasers.transform.position.x - 10, 0.001f),Lasers.transform.position.y, Lasers.transform.position.z);
		}
		if(yes){
			TrapWalls.transform.position = new Vector3(transform.position.x, Mathf.Lerp(TrapWalls.transform.position.y, transform.position.y-5, 0.01f), transform.position.z);
		} 
		if(no){
			TrapWalls.transform.position = new Vector3(transform.position.x, Mathf.Lerp(TrapWalls.transform.position.y, transform.position.y, 0.01f), transform.position.z);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			no = true;
			yes = false;
			StartLasers();
		}
	}

	public void StartLasers(){
		for(int i = 0; i < Lasers.transform.childCount; i++){
			StartCoroutine(inst_laser(i));
		}
	}

	IEnumerator inst_laser(int time){
		yield return new WaitForSeconds(time);
		Lasers.transform.GetChild(time).gameObject.SetActive(true);
		if(time >= Lasers.transform.childCount-1){
			UIManager.StartTrap(this);
			LasersOn = true;
		}
	}

	public void StopLasers(){
		for(int i = 0; i < Lasers.transform.childCount; i++){
			Lasers.transform.GetChild(i).gameObject.SetActive(false);
		}
	}

	public void FinishTrap(){
		LasersOn = false;
		no = false;
		yes = true;	
		StopLasers();	
	}
}
