using UnityEngine;
using System.Collections;



public class BeeBehavior1 : MonoBehaviour {

	public GameObject stinger;
	public float speed;
	public Animator anim;
	public float stingerInterval;
	public bool shooted = false;
	public Transform StingerPoint;
	public AudioClip dieSound;
	public AudioClip shootSound;


	private GameObject Player;
	private PlayerBehavior playerScript;
	private Vector3 firstPosition;

	private float deltaX;
	private float deltaY;
	private float deltaZ;
	private bool dead = false;
	private float deltaTime = 0f;
	private float animTimeTracker = 0f;


	private bool stingerShooted = false;


//	public Vector3 stingerOffset = new Vector3(0.1f,0.0f,0.4f);

	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		playerScript = Player.GetComponent<PlayerBehavior> ();
		firstPosition = transform.localPosition;
		LookAtPlayer (); //face the player
		//define the area the bee can move
		anim = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {
		if (shooted == false) {
			deltaTime += Time.deltaTime;
			if (playerScript.playerState < 2) {
				animTimeTracker += Time.deltaTime;
			}
			GetComponent<AudioSource>().Play ();
			GetXYDisplacement ();
			transform.Translate (deltaX, deltaY, 0);
			LookAtPlayer ();
			transform.Rotate (0, 10, 0);
			ShootStinger ();
			StartCoroutine ("CreateStinger");
			stingerShooted = false;

		}else{
			anim.Play("Death1");
			StartCoroutine ("KillBee");
			if (dead)
			{
				transform.position = Vector3.MoveTowards (transform.position, new Vector3(transform.position.x,-10,transform.position.z) , 0.5f); //fall down
			}
		}
	}

	void LookAtPlayer(){
			transform.LookAt(Player.transform);
	}

	void GetXYDisplacement(){
		// if n second passed since last change in movement, change it
		if (deltaTime > 2.0f) {
			deltaX = Random.Range (-speed, speed);
			deltaY = Random.Range (-speed, speed);
			deltaZ = Random.Range (-speed, speed);
			deltaTime = 0.0f;
		}	
		// if bee is far from its starting position, change direction
		if (Vector3.Distance (firstPosition, transform.position) > 2) {
			deltaX = -deltaX;
			deltaY = -deltaY;
			deltaZ = -deltaZ;
		}
	}
		
	void ShootStinger(){
		if (animTimeTracker > stingerInterval && playerScript.playerState <2 ) {
			anim.Play ("Hit1");
			AudioSource.PlayClipAtPoint (shootSound, transform.position);
			stingerShooted = true;
			print ("shoot stinger");
			animTimeTracker = 0.0f;
		}
	}

	public void hitSound(){
		AudioSource.PlayClipAtPoint (dieSound, transform.position);
	}
	IEnumerator CreateStinger (){
		if (stingerShooted == true) {
			yield return new WaitForSeconds (0.3f);
			Instantiate (stinger, StingerPoint.position, transform.rotation);
		}
	}

	IEnumerator KillBee (){
		yield return new WaitForSeconds (0.6f);
		dead = true;
		yield return new WaitForSeconds (1.4f);
		anim.Stop ();
		playerScript.decrementNumberofBee();
		gameObject.SetActiveRecursively(false);
	}

}
