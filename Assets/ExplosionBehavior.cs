using UnityEngine;
using System.Collections;

public class ExplosionBehavior : MonoBehaviour {
	public AudioClip clip;
	// Use this for initialization
	void Start () {
		AudioSource.PlayClipAtPoint(clip,transform.position);
		Destroy (this.gameObject, 0.6f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
