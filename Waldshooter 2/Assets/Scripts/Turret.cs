using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	private Transform targetTransform; 
	private enum States { attackEnemy, onHold};
	float timeUntilnextShot = 0.5f; 

	int hp = 20; 
	int level = 1; 
	int costs = 100; 
	int upgradecosts = 50; 

	private States myState; 


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


}
