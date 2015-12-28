﻿ using UnityEngine;
using System.Collections;

public class TileGenerator : MonoBehaviour {

	int mapCol;
	int mapRow;
	//Tile[] tileTypes;
	Quaternion tileRot = Quaternion.Euler(270, 0, 0);


	int[,] tileMap = new int[,]
	{
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
		{0, 0, 0, 0, 0, 0, 1, 5, 5, 5, 5, 5, 0, 0, 0, 0, 0 ,0 ,0 ,0},
	};

	// Prefab resources
	string[] prefabResources = new string[6] {
		"Dirt_Block",
		"Dirt_Ramp",
		"Dirt_Slab",
		"Grass_Block",
		"Grass_Ramp",
		"Grass_Slab"
	};

	void Start () {
		int mapCol = tileMap.GetUpperBound(1);
		int mapRow = tileMap.GetUpperBound(0);

		for (int x = 0; x <= mapRow; x++) {
			for (int z = 0; z <= mapCol; z++) {
				int tileIndex = tileMap[x, z];

				// Get already created prefab from Unity and position/rotate it
				Vector3 tilePos = new Vector3 (x, 0, z);
				GameObject prefab = Resources.Load(prefabResources[tileIndex]) as GameObject;

				GameObject tilePrefabGameObject = Instantiate(prefab, tilePos, tileRot) as GameObject;
			}
		}
	}
}
