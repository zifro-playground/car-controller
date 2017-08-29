using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Level {



	public static void Init() {

	}

	public static void Reset(){

	}

	public static void LoadLevel(TextAsset textLevel) {
		string[] rows = textLevel.text.Split ('\n');

		foreach (string row in rows) {
			
		}
	}
}
