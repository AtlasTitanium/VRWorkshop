using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dropBalls : MonoBehaviour {
	public GameObject ballPrefab;
	public float speed;
	public int counter;
	void Start () {
		StartCoroutine(dropBall());
	}

	IEnumerator dropBall(){
		yield return new WaitForSeconds(speed);
		Instantiate(ballPrefab,this.transform.position,this.transform.rotation);
		counter ++;
		StartCoroutine(dropBall());
	}
}
