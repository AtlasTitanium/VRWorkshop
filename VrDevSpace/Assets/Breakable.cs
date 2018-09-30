using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour {
	[Range (1,20)]
	public int strength = 2;
	public GameObject BrokenPrefab;
	public GameObject p_System;
	public AudioSource audioSource;
	public AudioClip audioPlayed;
	void OnCollisionEnter (Collision col)
    {
		if(col.transform.tag != "Player"){
			if(col.relativeVelocity.magnitude > strength){
				GameObject obj = Instantiate(BrokenPrefab, transform.position, transform.rotation);
				if(audioSource != null && audioPlayed != null){
					audioSource.PlayOneShot(audioPlayed);
				}
				if(p_System != null){
					GameObject PS = Instantiate(p_System, transform.position, Quaternion.EulerRotation(0,0,0));
					PS.GetComponent<ParticleSystem>().Play();
					if(PS.GetComponent<ParticleSystem>().isStopped){
						Destroy(PS);
					}
				}
			for(int i = 0; i < obj.transform.childCount; ++i){obj.transform.GetChild(i).GetComponent<Rigidbody>().velocity = transform.GetComponent<Rigidbody>().velocity;}
				this.gameObject.SetActive(false);
			}
		}
    }
}
