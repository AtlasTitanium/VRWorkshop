using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_Manager : MonoBehaviour {
	public GameObject GameOverScreen, Background;

	public Button TopRight, DownRight, Reset;
	

	public UI_Switcher Screen1, Screen2, Screen3;
	public PuzzleScreen PuzzleScreen1, PuzzleScreen2, PuzzleScreen3;
	public int HowManyPuzzles = 1;
	private TrapRoom trapRoom;
	private int StrikeCounter = 0;
	public Image Strike1, Strike2, Strike3;

	public Movement Movement;
	public GameObject Head, leftHand, rightHand;
	public Camera VRCam;

	void Start(){
		Button TopR = TopRight.GetComponent<Button>();
		Button DownR = DownRight.GetComponent<Button>();
		Button RESET = Reset.GetComponent<Button>();

		TopR.onClick.AddListener(NextUI);
        DownR.onClick.AddListener(PreviousUI);
		RESET.onClick.AddListener(RE);
	}
	public void StartTrap(TrapRoom traproom){
		trapRoom = traproom;
		int RandomPuzzle = Random.Range(1,HowManyPuzzles);
		PuzzleScreen1.StartPuzzle(RandomPuzzle, this);
		PuzzleScreen2.ShowPuzzle(RandomPuzzle, this);
		PuzzleScreen3.ShowPuzzle(RandomPuzzle, this);
	}
	public void FinishTrap(){
		trapRoom.FinishTrap();
	}

	private void NextUI(){
		Screen1.Next();
		Screen2.Next();
		Screen3.Next();
	}

	private void PreviousUI(){
		Screen1.Previous();
		Screen2.Previous();
		Screen3.Previous();
	}

	public void GameOver(){
		Screen1.gameObject.SetActive(false);
		Screen2.gameObject.SetActive(false);
		Screen3.gameObject.SetActive(false);
		Background.SetActive(false);
		GameOverScreen.SetActive(true);
	}

	private void RE(){
		Application.LoadLevel(Application.loadedLevel);
	}
	
	public void AddStrike(){
		StrikeCounter ++;
		switch(StrikeCounter){
			case 3:
				Strike3.color = Color.red;
				Die();
				GameOver();
				break;
			case 2:
				Strike2.color = Color.red;
				break;
			case 1:
				Strike1.color = Color.red;
				break;
			default:
				break;
		}
	}

	public void Die(){
		Movement.enabled = false;
		Head.transform.parent = null;
		leftHand.transform.parent = null;
		rightHand.transform.parent = null;
		LayerMask mask = 1 << 5;
		mask = ~mask;
		VRCam.cullingMask = mask;
		Head.GetComponent<Rigidbody>().useGravity = enabled;
		leftHand.GetComponent<Rigidbody>().useGravity = enabled;
		rightHand.GetComponent<Rigidbody>().useGravity = enabled;
		Head.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		leftHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		rightHand.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
		Head.GetComponent<Collider>().isTrigger = false;
		leftHand.GetComponent<Collider>().isTrigger = false;
		rightHand.GetComponent<Collider>().isTrigger = false;
	}
}
