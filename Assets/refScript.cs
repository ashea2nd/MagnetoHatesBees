using UnityEngine;
using System.Collections;

public class refScript : MonoBehaviour {
	public GameObject stinger;
	public Vector3 stingerPosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	//void Update () {
	
	//}

	public void CreateStinger(){
		Instantiate (stinger, stingerPosition, transform.rotation);
		print ("stinger instanciated");
	}
}
