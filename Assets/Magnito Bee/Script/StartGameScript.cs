using UnityEngine;
using System.Collections;

public class StartGameScript : MonoBehaviour {
	public TutorialTexts textScript;
	// Use this for initialization


	public void StartGame(){
		//textScript.NextText ();
		textScript.NextState ();
	}
}
