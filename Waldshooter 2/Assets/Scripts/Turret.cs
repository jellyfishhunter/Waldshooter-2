using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform currEnemy; 
	private enum States { attackEnemy, onHold};
	private States myState; 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
