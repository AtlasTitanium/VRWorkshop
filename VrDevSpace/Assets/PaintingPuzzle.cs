using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PaintingPuzzle : MonoBehaviour {
	public PuzzleScreen pScreen, pScreenTop, pScreenBottom;
	public Button Wrong1, Right, Wrong2, Wrong3;
	public UI_Manager ui_manager;

	void Start(){
		Button w1 = Wrong1.GetComponent<Button>();
		Button w2 = Wrong2.GetComponent<Button>();
		Button w3 = Wrong3.GetComponent<Button>();
		Button r = Right.GetComponent<Button>();
		
		w1.onClick.AddListener(delegate {Addstrike(Wrong1.GetComponentInChildren<Text>()); });
        w2.onClick.AddListener(delegate {Addstrike(Wrong2.GetComponentInChildren<Text>()); });
		w3.onClick.AddListener(delegate {Addstrike(Wrong3.GetComponentInChildren<Text>()); });
		r.onClick.AddListener(Win);
	}
	
	private void Addstrike(Text wrongAwnser){
		wrongAwnser.color = Color.red;
		ui_manager.AddStrike();
	}

	private void Win(){
		Right.GetComponentInChildren<Text>().color = Color.yellow;
		pScreenTop.UnShowPuzzle();
		pScreenBottom.UnShowPuzzle();
		pScreen.FinishPuzzle();
	}
}
