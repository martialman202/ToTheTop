using UnityEngine;
using System.Collections;

public class Manager : Singleton<Manager> {
	protected Manager () {} // guarantee this will be always a singleton only - can't use the constructor!
	
	public float treeHeight = 0;
	public float monkeyHeight = 0;

	public string levelFileName;

	public int prevLevel;

	public int numLevels = 10;
	public int levelIndex = 0;
	public string [] levels;

	void Start() {
		levels = new string[numLevels];
		levels [0] = "easy0";
		levels [1] = "easy1";
		levels [2] = "easy2";
		levels [3] = "easy3";
		levels [4] = "easy4";
		levels [5] = "med0";
		levels [6] = "med1";
		levels [7] = "med2";
		levels [8] = "med3";
		levels [9] = "hard0";

		numLevels = levels.Length;
		levelIndex = 0;
		levelFileName = levels [levelIndex];
	}
}
