using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	private GameObject player; 
	private float zDistance; 

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player"); 
		zDistance = player.transform.position.z - this.transform.position.z; 
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 myPos = this.transform.position; 
		myPos.x = player.transform.position.x; 
		myPos.z = player.transform.position.z - zDistance; 


		this.transform.position = Vector3.Lerp(transform.position, myPos, 2.0f* Time.deltaTime);


	}
}
