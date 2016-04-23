using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Turret : MonoBehaviour {


	public List<GameObject> enemysInTrigger;  

	private Transform targetTransform; 
	private enum States { attackEnemy, onHold};
	float timeUntilnextShot = 0.5f; 
	float attackIntervall = 0.5f; 
	float range = 20f; 

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
		if (myState == States.attackEnemy) {
		
		}
		if (myState == States.onHold) {
			
		}
	}

	void OnTriggerEnter(Collider other) {
			enemysInTrigger.Add (other.gameObject); 
	}

}
