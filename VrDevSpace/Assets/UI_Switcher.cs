using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Switcher : MonoBehaviour {

	public int currentUI = 1;
	public GameObject Screen1, Screen2, Screen3;
	public void Next(){
		currentUI ++;
		if(currentUI >= 4){
			currentUI = 1;
		}
	}
	public void Previous(){
		currentUI --;
		if(currentUI <= 0){
			currentUI = 3;
		}
	}

	private void Update(){
		switch(currentUI){
			case 3:
				Screen3.SetActive(true);
				Screen2.SetActive(false);
				Screen1.SetActive(false);
				break;
			case 2: 
				Screen3.SetActive(false);
				Screen2.SetActive(true);
				Screen1.SetActive(false);
				break;
			case 1:
				Screen3.SetActive(false);
				Screen2.SetActive(false);
				Screen1.SetActive(true);
				break;
			default:
				Debug.Log("UI out of range");
				break;
		}
	}
}
