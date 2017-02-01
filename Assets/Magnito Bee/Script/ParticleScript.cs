using UnityEngine;
using System.Collections;

public class ParticleScript : MonoBehaviour {


	AudioSource gokuAudio;

	void Start(){
		gokuAudio = GetComponent<AudioSource> ();
	}

	void Update(){
		if (Input.GetKeyDown ("x")) {
			turnParticlesOn();
		}
		if (Input.GetKeyDown ("c")) {
			turnParticlesOff();
		}
	}

	public void turnParticlesOn(){
		gameObject.GetComponent<ParticleSystem> ().Play ();
		gokuAudio.Play ();

	}

	public void turnParticlesOff(){
		gameObject.GetComponent<ParticleSystem> ().Stop ();
	}


}
