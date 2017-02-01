using UnityEngine;
using System.Collections;

public class TutorialStinger : MonoBehaviour {

	//public Transform player;
	//public Transform controllerPointer;
	public GameObject panel;
	public float speed ;
	public Vector3 playerHoldingOffset;

	public GameObject explosion;
	//public float shootingSpeed = 1000.0f;
	private tutorialBeeBehavior beeScript;
	//public Vector3 pointer;
	public Transform reference;
	private GameObject player;
	private GameObject pointer;
	private Vector3 shootingDestination;
	public tutorialPlayer playerScript;
	public TutorialTexts textScript;
	//public AudioClip shootedSound;
	//private AudioSource audio;
	public AudioClip windSound;
	public AudioClip stabSound;
	public AudioClip stopSound;
	public AudioClip comingSound;
	public AudioClip shootingSound;
	public Shader glow;
	private Shader normal;
	//private Rigidbody rb;
	//private GameObject controllerPointer;

	public int stingerState = 0; //0:coming, 1:getting 2:holding, 2:shooting

	// Use this for initialization
	void Start () {
		//rb = GetComponent<Rigidbody>();
		panel = GameObject.Find("panel");
		normal = GetComponent<Renderer>().material.shader;
		GetComponent<AudioSource>().loop = true;
		GetComponent<AudioSource>().clip = windSound;
		GetComponent<AudioSource>().Play ();
		reference = GameObject.Find("ref").GetComponent<Transform>();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerScript = player.GetComponent<tutorialPlayer> ();
		textScript = GameObject.FindGameObjectWithTag ("tutorial").GetComponent<TutorialTexts> ();
		//AudioSource audio = GetComponent<AudioSource> ();
		pointer = GameObject.FindGameObjectWithTag ("ControllerPointer");
		transform.parent = reference.transform;
		//controllerPointer = GameObject.FindGameObjectWithTag ("ControllerPointer");
		//set transform parent to the playr so that stinger does not move with bee
		LookAtPlayer ();
	}

	// Update is called once per frame
	void Update () {

			if (stingerState == 0) {
				comingUpdate ();
			}
			if (stingerState == 1) {
				stoppingUpdate ();
				GlowThis ();
			}
			if (stingerState == 2) {
				gettingUpdate ();
			}
			if (stingerState == 3) {
				holdingUpdate ();
			}
			if (stingerState == 4) {
				shootingUpdate ();
			}
		if (stingerState == 5) {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (this.gameObject);
		}
	}


	void comingUpdate(){
		float step = speed * Time.deltaTime;
		//audio.Play ();
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position, step); //move stinger to the player with constant speed
		if (Input.GetKeyDown("1")){
			StoppingStinger ();
			stingerState = 1;
		}

		if (Vector3.Distance(player.transform.position,transform.position) < 0.1f){
			print("Stinger Destroyed");
			//audio.clip = shootedSound;
			;
			GetComponent<AudioSource>().Stop();
			AudioSource.PlayClipAtPoint(stabSound,transform.position);
			print ("played");
			playerScript.makeBeeShoot ();
			Destroy(this.gameObject);
		}
	}

	void stoppingUpdate(){
		GetComponent<AudioSource>().Stop ();
		//transform.position = transform.position;
		if (GvrController.Gyro[0] > 0.5){
			stingerState = 2;
			AudioSource.PlayClipAtPoint (comingSound, transform.position);
			transform.Rotate (0f, 180f, 0f);
		}
		if (Input.GetKeyDown("2")){
			stingerState = 2;
			transform.Rotate (0f, 180f, 0f);
		}
	}

	void gettingUpdate(){
		float step = speed * 10.0f * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, player.transform.position + playerHoldingOffset, step); //move stinger to the player with constant speed
		if (Vector3.Distance(player.transform.position+playerHoldingOffset,transform.position) < 0.3){
			if (textScript.instructionState == 3) {
				print ("fuck you2");
				StartCoroutine ("NextInstruction");
			}
			stingerState = 3;
		}
	}
	void holdingUpdate(){
		transform.position = player.transform.position + playerHoldingOffset; //fix position of the stinger near the player. 
		LookAtPointer();
		if (Input.GetKeyDown("3")){
//			shootingDestination = 2*pointer.transform.position-transform.position;
//			stingerState = 4;
			ShootingStinger();
		}

	}
	void shootingUpdate(){
		//use add force instead
		float step = speed * 10.0f * Time.deltaTime;
		transform.position = Vector3.MoveTowards (transform.position, shootingDestination , step); //move stinger to the player with constant speed
		//rb.AddForce ((shootingDestination-transform.position) * speed);
		//print ("shooted");
		if (transform.position == shootingDestination) {
			playerScript.makeBeeShoot ();
			Destroy (this.gameObject);
		}
	}
	void collidedUpdate(){

	}



	void LookAtPlayer(){
		if(player != null)
		{
			transform.LookAt(player.transform); 
			transform.Rotate (180, 0, 0);
		}
	}

	void LookAtPointer(){
		if (pointer != null) {
			transform.LookAt (pointer.transform);
			transform.Rotate (180, 0, 0);
		}
	}

	public void StoppingStinger(){
		stingerState += 1;
		playerScript.stingerStopSoundPlay ();
		if (textScript.instructionState == 2) {
			print ("yes");
			StartCoroutine ("NextInstruction");
		}

	}

	public void ShootingStinger(){
		if (stingerState == 1) {
			stingerState = 0;
		}else if(stingerState != 3)
		{
			stingerState = 0;
		}else {
			shootingDestination = 4*pointer.transform.position-3*transform.position;
			stingerState = 4;
			AudioSource.PlayClipAtPoint (shootingSound, transform.position);
		}
	}

	public void GlowThis(){
		GetComponent<Renderer>().material.shader = glow;
	}

	public void stopGlowing(){
		GetComponent<Renderer>().material.shader = normal;
	}

	void OnTriggerEnter(Collider other) 
	{
		if (other.gameObject.CompareTag ("Bee"))
		{
			if (stingerState == 4 ) {
				print ("aaaaa");
				StartCoroutine("ShowingCanvas");
				print ("Bee shooted");
				//other.gameObject.SetActive (false);
				beeScript = other.gameObject.GetComponent<tutorialBeeBehavior>();
				beeScript.shooted = true;
				beeScript.hitSound ();
				Rigidbody rb = other.GetComponent<Rigidbody> ();
				Vector3 movement = shootingDestination - transform.position;
				rb.AddForce (movement * 300.0f);

			}
		}
		if (other.gameObject.CompareTag("Stinger")){
			print ("Stinger collide");
			StingerBehavior stingerScript = other.GetComponent<StingerBehavior> ();
			stingerScript.stingerState = 5;
		}
	}

	IEnumerator NextInstruction (){
		yield return new WaitForSeconds (1.0f);
		textScript.showCanvas ();
		Destroy (this.gameObject);
	}

	IEnumerator ShowingCanvas(){
		yield return new WaitForSeconds (1.0f);
		textScript.showCanvas ();	
	}

}
