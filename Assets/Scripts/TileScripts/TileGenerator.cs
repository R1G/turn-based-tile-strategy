using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class TileGenerator : MonoBehaviour {

	public static int mapCol;
	public static int mapRow;

	public static GameObject [,] tilesRef;	// Holds a reference to every instantiated tile
	public static int range = 6;
	public static List<GameObject> highlightedTiles = new List<GameObject>(); // Holds a reference to every highlighted tile

	Quaternion tileRot = Quaternion.Euler(0, 0, 0);

	string[] tileTypes = new string[] {
		
		"FFC40F", // dirt – walkable – red team outer ground
		"789D4C", // grass – walkable – center and alternate corners
		"FF8A00", // lava- walkable – red team base ground
		"E9BA00", // shrub – walkable – area surrounding bush outside red team
		"737373", // shrinestone – walkable – top left & bottom right corner base center
		"9D694C", // road – walkable – red team road/path
		"5D442A", // bridge – walkable – red team bridge/crossing
		"295C55", // crossing – walkable – blue team bridge/crossing
		"D7D7D7", // gravel – walkable – center area alternate quadrants
		"909090", // crossroad – walkable – center area crossroads
		"3B949E", // tundra – walkable – blue team outer ground
		"ACC5BE", // path – walkable – blue team road/path
		"FBBB10", // red buff – walkable – center area lower bottom quadrant
		"04E2FD", // blue buff – walkable – center area top right quadrant
		"397980", // fog – walkable – fog around menhirs outside blue team base
		"0096FF", // ice – walkable – blue team base ground
		"C5BEAC", // water - empty
		"58623C", // tree - nonwalkable
		"595959", // shrine - nonwalkable
		"005B9B", // rampart – nonwalkable – blue team wall
		"727272", // pillar – nonwalkable – center area alternate quadrant prop
		"3C7B7D", // teal house – non-walkable – center area house of the Teal family
		"727B67", // gray house – non-walkable – center area house of the Gray family
		"816A39", // brown house – non wallable – center area house of the Indian family
		"5D5D5D", // fountain – non walkable – center most artifact (animated fountain)
		"1CC0D3", // glow stone – non walkable – blue team ground glowing artifact
		"39666F", // menhir – nonwalkable – menhirs outside blue team base
		"C26900", // wall – nonwalkable – red team wall
		"FF770B", // brazier – nonwalkable – red team ground glowing artifact
		"BA960A"  // bush – nonwalkable – bush outside red team
	};

	// Prefab resources
	string[] prefabResources = new string[] {
		
		"dirt", 			// walkable – red team outer ground
		"grass", 			// walkable – center and alternate corners
		"lava", 			// walkable – red team base ground
		"shrub", 			// walkable – area surrounding bush outside red team
		"shrineStone", 		// walkable – top left & bottom right corner base center
		"road", 			// walkable – red team road/path
		"bridge", 			// walkable – red team bridge/crossing
		"crossing", 		// walkable – blue team bridge/crossing
		"gravel", 			// walkable – center area alternate quadrants
		"crossRoad", 		// walkable – center area crossroads
		"tundra", 			// walkable – blue team outer ground
		"path", 			// walkable – blue team road/path
		"redBuff", 		    // walkable – center area lower bottom quadrant
		"blueBuff", 		// walkable – center area top right quadrant
		"fog", 				// walkable – fog around menhirs outside blue team base
		"ice",				// walkable – blue team base ground
		"water", 			// empty
		"tree", 			// nonwalkable
		"shrine", 			// nonwalkable
		"rampart", 			// nonwalkable – blue team wall
		"pillar", 			// nonwalkable – center area alternate quadrant prop
		"tealHouse", 		// non-walkable – center area house of the Teal family
		"grayHouse", 		// non-walkable – center area house of the Gray family
		"brownHouse", 		// non wallable – center area house of the Indian family
		"fountain", 		// non walkable – center most artifact (animated fountain)
		"glowStone", 		// non walkable – blue team ground glowing artifact
		"menhir", 			// nonwalkable – menhirs outside blue team base
		"wall", 			// nonwalkable – red team wall
		"brazier",		    // nonwalkable – red team ground glowing artifact
		"bush"  			// nonwalkable – bush outside red team
	};

	void Start () {

		// Load an image of a map and use its pixels as the array
		Texture2D levelBitmap = Resources.Load("mapPlan") as Texture2D;
		mapRow = levelBitmap.height;
		mapCol = levelBitmap.width;

		// Hold a reference to every instantiated tile in a new game object
		tilesRef = new GameObject[mapRow, mapCol];

		// Loop through the pixels and draw the map based on each pixel's color
		for (int x = 0; x < mapRow; x++) {
			for (int z = 0; z < mapCol; z++) {
				// Get the color of the current pixel
				Color c = levelBitmap.GetPixel(z, x);

				// Convert the color (which will be RGB) to its hex value
				string hex = ColorUtility.ToHtmlStringRGB(c);

				// Obtain a tile index from tile types to get its prefab resource
				int tileIndex = System.Array.IndexOf(tileTypes, hex);
				Vector3 tilePos = new Vector3 (x, 0, z);
				GameObject prefab = Resources.Load(prefabResources[tileIndex]) as GameObject;
				if (tileIndex > 15) {
					prefab.tag = "unwalkableTile";
				} else {
					prefab.tag = "walkableTile";
				}

				// Instantiate a tile and store it in the tiles ref for later use
				tilesRef[x, z] = Instantiate(prefab, tilePos, tileRot) as GameObject;
			}
		}
	}

	// Highlight an area of tiles around the selected troop
	public static void highlightTilesInRange(int x, int z) {
		unhighlightTiles();

		List<GameObject> tiles = GetWalkableTilesInRange(x, z, range);
		foreach (GameObject tile in tiles) {
			int deltaX = Mathf.Abs((int)tile.transform.position.x - x);
			int deltaZ = Mathf.Abs((int)tile.transform.position.z - z);
			if (deltaX + deltaZ <= range) {
				tile.GetComponent<MeshRenderer> ().material.color = Color.green;
				highlightedTiles.Add (tile);
			}
		}
	}

	public static void unhighlightTiles() {
		if (highlightedTiles.Count > 0) {
			foreach (GameObject tile in highlightedTiles) {
				tile.GetComponent<MeshRenderer> ().material.color = Color.white;
			}
			highlightedTiles.Clear();
		}
	}

	public static List<GameObject>GetWalkableTilesInRange(int xPos, int zPos, int range) {

		unhighlightTiles ();
		List<GameObject> openTiles = new List<GameObject> ();
		List<GameObject> walkableTiles = new List<GameObject> ();
		for (int x = -1; x <= 1; x++) {
			for (int z = -1; z <= 1; z++) {
				if (x == 0 && z == 0) {
					continue;
				} else if (tilesRef [xPos + x, zPos + z].tag == "walkableTile" && tilesRef [xPos + x, zPos + z] != null) {
					openTiles.Add (tilesRef [xPos + x, zPos + z]);
					walkableTiles.Add (tilesRef [xPos + x, zPos + z]);

				}
			} 
		}
			for (int t = 0; t < range; t++) {
			foreach (GameObject tile in openTiles.ToArray()) {
					int tileX = (int)tile.transform.position.x;
					int tileZ = (int)tile.transform.position.z;
					
					for (int x = -1; x <= 1; x++) {
						for (int z = -1; z <= 1; z++) {
							if (x == 0 && z == 0) {
								continue;
						} if (tileX + x <= 63 && tileZ + z <= 63 && tileX + x >= 0 && tileZ + z >= 0) {
							if (tilesRef [tileX + x, tileZ + z].tag == "walkableTile" && !openTiles.Contains(tilesRef [tileX + x, tileZ + z])) {
								walkableTiles.Add (tilesRef [tileX + x, tileZ + z]);
							}
						} 
					} 
				}
			}
			openTiles = walkableTiles;
		}
		return walkableTiles;
	}

}