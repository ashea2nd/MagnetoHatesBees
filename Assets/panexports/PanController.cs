using UnityEngine;
using System.Collections;
using UnityEngine.UI; 

public class PanController : MonoBehaviour {

	// Use this for initialization
	public bool inSwatMode = true;
	public PlayerBehavior playerScript;
	public GameObject panBase; 
	public AudioClip hitSound;
	private int x = 0;
	private Vector3 currentAccel = GvrController.Accel;
	private Vector3 lastAccel = GvrController.Accel;

	private bool isLooking = false;
	private GameObject beeObject;
	private Vector3 beePosition;
	private bool alreadyMade = false;


	public HealthAndEnergyScript HealthAndEnergyUI;
	void Start () {
		
		
	}
	
	// Update is called once per frame

	void Update () {
		//When changing the energyCoeff to account for swatting and non-swatting modes, make sure you include a separate boolean. 
		//the "testMode" boolean is needed, otherwise it just instantly overwrites the previous update. 
//		if (Input.GetKeyDown ("l")) {
//			HealthAndEnergyUI.removeLifePoint ();
//			HealthAndEnergyUI.changeEnergyCoeff(0.0f);
//			testMode = true;
//		}
		if (playerScript.playerState == 3) {
//			if (!testMode) {
//				HealthAndEnergyUI.changeEnergyCoeff (8.0f);
//			}
			lastAccel = currentAccel;
			currentAccel = GvrController.Accel;
			if (isLooking) {
				//if (Input.GetKeyDown("s")) {
				if (Mathf.Abs (lastAccel.z - currentAccel.z) > 1.0f) {
					if (!alreadyMade) {
						alreadyMade = true;
						GameObject pan = GameObject.Instantiate (panBase, beePosition + new Vector3 (0, 0.5f, -6.5f), Quaternion.Euler (0, 0, 0)) as GameObject;
						//x += 10;
						StartCoroutine(swatBee (pan, beeObject));
					}
				}
			}
		}

	}

	public void shouldSwatBee(GameObject bee){
		//Debug.Log ("LookedON");
		//Debug.Log (isLooking);
		isLooking = true;
		//Debug.Log (isLooking);
		beePosition = bee.transform.position;
		beeObject = bee;
//		Debug.Log (beeObject);
	}

	public void lookAwayFromBee(){
		Debug.Log ("Looked Away");
		isLooking = false;
		//alreadyMade = false;
		beeObject = null;
	}

	IEnumerator swatBee(GameObject pan, GameObject bee){
		pan.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("SwatPan");
		AudioSource.PlayClipAtPoint (hitSound, transform.position);
		bee.GetComponent<BeeBehavior1> ().shooted = true;
		yield return new WaitForSeconds(0.5f);
		bee.SetActive (false);
		yield return new WaitForSeconds (0.1f);
		pan.SetActive (false);
		alreadyMade = false;
	}
}
