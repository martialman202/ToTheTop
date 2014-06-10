using UnityEngine;
using System.Collections;

public class Manager : Singleton<Manager> {
	protected Manager () {} // guarantee this will be always a singleton only - can't use the constructor!
	
	public float treeHeight = 0;
	public float monkeyHeight = 0;
	public float monkeySpeed = 0;

	public string levelFileName = "test";

	public int prevLevel;

	public int numLevels = 9;
	public int levelIndex = 0;
	public string [] levels;

	public bool onTree;
	public int score;

	public string difficulty; //either easy, med, hard

	void Start() {
		levels = new string[numLevels];
		levels [0] = "easy0";
		levels [1] = "easy1";
		levels [2] = "easy2";
		levels [3] = "med0";
		levels [4] = "med1";
		levels [5] = "med2";
		levels [6] = "hard0";
		levels [7] = "hard1";
		levels [8] = "hard2";

		numLevels = levels.Length;
		levelIndex = 0;
		levelFileName = levels [levelIndex];
	}
}
