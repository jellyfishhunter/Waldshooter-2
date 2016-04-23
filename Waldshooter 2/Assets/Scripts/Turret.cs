using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Turret : MonoBehaviour {


	public List<Transform> enemysInTrigger;  

	private Transform targetTransform; 
	private enum States { attackEnemy, onHold};

	float timeUntilnextShot = 0.5f; 
	float attackIntervall = 0.5f; 
	float range = 20f; 

	int hp = 100; 
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
			if(targetTransform != null)
			face (targetTransform.position); 
		}
		if (myState == States.onHold) {
			face (targetTransform.position); 
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			enemysInTrigger.Add (other.transform); 
		}
			
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Enemy") {
			enemysInTrigger.Remove (other.transform); 
		}

	}
		

	public void face(Vector3 t)
	{
		Vector3 direction = (new Vector3(t.x, transform.position.y, t.z) - transform.position).normalized;
		if (direction != Vector3.zero)
		{
			Quaternion lookRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10f);
		}
	}

	public void hit(GameObject bullet)
	{
			hp -= bullet.GetComponent<Bullet>().hitValue;
		if (hp <= 0)
		{
			die();
		}
	}

	void die()
	{
		Destroy(gameObject);
	}

}
