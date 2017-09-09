using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TemporaryDescriptions : MonoBehaviour, IPMLevelChanged {

	public Text text;

	public string[] description;

	public void OnPMLevelChanged() {
		text.text = description [PMWrapper.currentLevel];
	}
}
