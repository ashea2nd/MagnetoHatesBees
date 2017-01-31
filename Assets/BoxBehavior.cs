using UnityEngine;
using System.Collections;

public class BoxBehavior : MonoBehaviour {

	//private GameObject Player;
	// Use this for initialization
	void Start () {
		//Player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void BoxStartGame(){
		//Application.LoadLevel ("MainScene");
		this.gameObject.SetActive (false);
	}
	public void BoxChangeColor(){
		this.GetComponent<Renderer>().material.color = new Color(0.5f,1,1); //C#;
	}
}
