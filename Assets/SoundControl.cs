using UnityEngine;
using System.Collections;

public class SoundControl : MonoBehaviour {
	public AudioClip BGM;
	public AudioClip beastBGM;
	private AudioSource audio;

	// Use this for initialization
	void Start () {
		audio = GetComponent<AudioSource> ();
		audio.loop = true;
		audio.volume = 0.5f;
		audio.clip = BGM;
		audio.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void playNormalBGM(){
		audio.volume = 0.5f;
		audio.clip = BGM;
		audio.Play ();
		print ("normal bgm called");
	}
	public void playBeastBGM(){
		//audio.volume = 1.0f;
		audio.clip = beastBGM;
		audio.Play ();
	}
}
