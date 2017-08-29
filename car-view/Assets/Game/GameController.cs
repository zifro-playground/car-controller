using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PM;

public class GameController : MonoBehaviour, IPMCompilerStopped, IPMLevelChanged {

	public List<TextAsset> textLevel = new List<TextAsset>();


	void Start () {		
		PMWrapper.numOfLevels = textLevel.Count;
	}


	void Update () {
		
	}

	public void OnPMCompilerStopped (HelloCompiler.StopStatus status) {
		Level.LoadLevel(textLevel[PMWrapper.currentLevel]);
	}

	public void OnPMLevelChanged () {
		Level.LoadLevel (textLevel [PMWrapper.currentLevel]);
	}
}
