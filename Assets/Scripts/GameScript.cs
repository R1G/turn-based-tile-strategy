using UnityEngine;
using System.Collections;

public class GameScript : MonoBehaviour {

	static public string turn = "PlayerTurn";
	public static int playerMoves = 5;

	public static void playerTurn() {
		playerMoves--;
		if (playerMoves == 0) {
			turn = "EnemyTurn";
			playerMoves = 5;
		} 
	}
}
