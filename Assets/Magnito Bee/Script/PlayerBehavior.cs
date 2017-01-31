using UnityEngine;
using System.Collections;

public class PlayerBehavior : MonoBehaviour {

	public Vector3 normalModePosition;
	public Vector3 beastModePosition;
	public HealthAndEnergyScript energyScript;
	public DemoInputManager InputManagerScript;
	public GameObject laser;
	//private bool beastMode = false;
	//private bool transiteComplete = false;
	public int playerState = 0; //0:normal,1:charged 2: transit to beast mode, 3:beastmode, 4:transit back to normal
	public int numberOfBees = 5;
	public AudioClip stingerStopSound;
	public AudioClip winningSound;
	public SoundControl soundScript;
	public ParticleScript particleScript;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (playerState == 1) {
			particleScript.turnParticlesOn ();
			playerState = 2;
		} else if (playerState == 2) {
			transform.position = Vector3.MoveTowards (transform.position, beastModePosition, 0.1f);
			if (transform.position == beastModePosition) {
				//InputManagerScript.SetControllerInputActive (false);
				InputManagerScript.SetGazeInputActive (true);
				laser.SetActive (false);
				particleScript.turnParticlesOff ();
				playerState = 3;
				soundScript.playBeastBGM ();
			}
		} else if (playerState == 3) {
			energyScript.decrementEnergy (10.0f);
		} else if (playerState == 4) {
			transform.position = Vector3.MoveTowards (transform.position, normalModePosition, 0.1f);
			if (transform.position == normalModePosition) {
				InputManagerScript.SetGazeInputActive (false);
				laser.SetActive (true);
				playerState = 0;
				soundScript.playNormalBGM ();
			}
		}
		if (numberOfBees == 0) {
			GetComponent<AudioSource> ().Stop ();
			AudioSource.PlayClipAtPoint (winningSound, transform.position);
		}
	}
	public void stingerStopSoundPlay(){
		AudioSource.PlayClipAtPoint (stingerStopSound,transform.position);
	}
	public void decrementNumberofBee(){
		numberOfBees = numberOfBees - 1;
	}
}
