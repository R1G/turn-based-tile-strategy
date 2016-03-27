using UnityEngine;
using System;
using System.Collections;

public class BaseTile : MonoBehaviour {

	private GameObject selectedTroop;
	private bool isWithinTravelRange;
	public int gCost;
	public int hCost;
	public GameObject parent;

	void OnMouseUp() {
		FindSelectedTroop();
		if(selectedTroop != null) {
			CheckRange();
			if(isWithinTravelRange) {
				MoveSelectedTroop();
				selectedTroop.tag = "FriendlyTroop";
				TileGenerator.unhighlightTiles ();
			
			}
		}
	}

	void FindSelectedTroop() {
		selectedTroop = GameObject.FindGameObjectWithTag ("SelectedTroop");
	}

	void CheckRange() {
		if (TileGenerator.highlightedTiles.Contains(gameObject)) {
			isWithinTravelRange = true;
		}
	}


	void MoveSelectedTroop() {
		float tileX = gameObject.transform.position.x;
		float tileZ = gameObject.transform.position.z;

		selectedTroop.transform.position = new Vector3 (tileX, 0, tileZ);
		GameScript.playerTurn ();
	}

	public int fCost {
		get { 
			return hCost + gCost;
		}
	}
}
