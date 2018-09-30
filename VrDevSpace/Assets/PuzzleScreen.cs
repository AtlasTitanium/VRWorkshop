using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleScreen : MonoBehaviour {
	public GameObject PlaceHolderText, Puzzle;
	private UI_Manager UIManager;


	public void StartPuzzle(int whichPuzzle, UI_Manager manager){
		UIManager = manager;
		PlaceHolderText.SetActive(false);
		if(whichPuzzle >= 1){
			//normally instantiate the puzzle through a prefab, but for now just activate the given puzzle
			Puzzle.SetActive(true);
		}
	}

	public void ShowPuzzle(int whichPuzzle, UI_Manager manager){
		UIManager = manager;
		PlaceHolderText.SetActive(false);
		if(whichPuzzle >= 1){
			Puzzle.SetActive(true);
		}
	}

	public void UnShowPuzzle(){
		PlaceHolderText.SetActive(true);
		Puzzle.SetActive(false);
	}
	public void FinishPuzzle(){
		PlaceHolderText.SetActive(true);
		UIManager.FinishTrap();
		Puzzle.SetActive(false);
	}
}
