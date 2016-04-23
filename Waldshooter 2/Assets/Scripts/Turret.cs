using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public Transform bullet; 
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
		if (myState = States.attackEnemy) {
		
		}
		if (myState = States.onHold) {
		
		}
	}

	Transform chooseTarget()
	{

		GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

		foreach (GameObject building in buildings)
		{
			if (inRangeOf(building))
			{
				return building.transform;
			}
		}

		return GameObject.FindGameObjectWithTag("Base Tree").transform;
	}

	bool inRangeOf(GameObject other)
	{
		return (Vector3.Distance(other.transform.position, transform.position) <= range);
	}

	void aimAndShoot()
	{
		// aim
		face(targetTransform.position);

		// range check
		bool targetInRange = Vector3.Distance(targetTransform.position, transform.position) <= range;

		// reload and shoot
		if (timeUntilnextShot <= 0 && targetInRange)
		{
			shoot();
			timeUntilnextShot = attackIntervall;
			Debug.Log("shoot");
		}

		timeUntilnextShot -= Time.deltaTime;
	}

	void shoot ()
	{
		Vector3 bulletSpawnPosition = transform.position + transform.forward;
		GameObject bulletInstance = Instantiate(bullet, bulletSpawnPosition, transform.rotation) as GameObject;
		Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
		bulletScript.isPlayerBullet = false;
		bulletScript.range = range;
	}

	public void hit(GameObject bullet)
	{
		hp -= bullet.GetComponent<Bullet>().hitValue;
		if (hp <= 0)
		{
			die();
		}
	}

	// TODO: enemy counter etc
	void die()
	{
		Destroy(gameObject);
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
}
