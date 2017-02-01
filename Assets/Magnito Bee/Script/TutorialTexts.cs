using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TutorialTexts : MonoBehaviour {

	//public refScript reference;
	public tutorialPlayer playerScript;
	public GameObject panel;
	public tutorialBeeBehavior beeScript;

//	public Texture slide1;
//	public Texture slide2;
//	public Texture slide3;
//	public Texture slide4;
//	public Texture slide5;
	public Texture[] slides;

	public int instructionState = 0;
	public Vector3 StingerPoint;
		
	// Use this for initialization
	void Start () {		
		gameObject.GetComponent<Renderer> ().material.mainTexture= slides[0];
//		gameObject.GetComponent<Renderer> ().material.mainTexture= slide1;
	}

	// Update is called once per frame
	void Update () {
	}

	public void NextState(){
		if (instructionState == 1 || instructionState == 2 || instructionState == 3) {
			hideCanvas ();
			beeScript.ShootStinger ();
		} else if (instructionState == 5) {
			SceneManager.LoadScene ("MainScene");
		}
		instructionState += 1;
		gameObject.GetComponent<Renderer> ().material.mainTexture= slides[instructionState];
		//playerScript.instructionState += 1;
	}

	public void hideCanvas(){
		panel.transform.Translate (0f, 0f, -1f);
		this.transform.Translate (0f, 0f, -1f);
	}

	public void showCanvas(){
		panel.transform.Translate (0f, 0f, 1f);
		this.transform.Translate (0f, 0f, 1f);
	}
//	public void hidePanel(){
//		
//	}
//
//	public void showPanel(){
//		panel.transform.Translate (0f, 0f, 1000f);
//	}

}
