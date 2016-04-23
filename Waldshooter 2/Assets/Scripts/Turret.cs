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
	public int costs = 100; 
	public int upgradecosts = 50; 

	private States myState; 


	public Transform Target;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f;

	public Transform Projectile;      
	private Transform myTransform;

	// Use this for initialization
	void Start () {
		myTransform = transform;
        StartCoroutine(SimulateProjectile());

    }

    // Update is called once per frame
    void Update () {
        Debug.DrawRay(Target.transform.position, Vector3.up);

    }

    void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
            Debug.Log("Enemy detected");
			Target = other.gameObject.transform; 
			StartCoroutine(SimulateProjectile()); 
		}

			
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Enemy") {
		}

	}

    void shoot()
    {

    }
		

	IEnumerator SimulateProjectile()
	{
		// Short delay added before Projectile is thrown
		yield return new WaitForSeconds(1.5f);
		Transform projectile = (Transform)Instantiate (Projectile, myTransform.position, Quaternion.identity);

		// Move projectile to the position of throwing object + add some offset if needed.
		projectile.position = myTransform.position + new Vector3(0, 2.0f, 0);

		// Calculate distance to target
		float target_Distance = Vector3.Distance(projectile.position, Target.position);

		// Calculate the velocity needed to throw the object to the target at specified angle.
		float projectile_Velocity = target_Distance / (Mathf.Sin(2 * firingAngle * Mathf.Deg2Rad) / gravity);

		// Extract the X  Y componenent of the velocity
		float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(firingAngle * Mathf.Deg2Rad);
		float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(firingAngle * Mathf.Deg2Rad);

		// Calculate flight time.
		float flightDuration = target_Distance / Vx;

		// Rotate projectile to face the target.
		projectile.rotation = Quaternion.LookRotation(Target.position - projectile.position);

		float elapse_time = 0;

		while (elapse_time < flightDuration)
		{
			projectile.Translate(0, (Vy - (gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime);

			elapse_time += Time.deltaTime;

			yield return null;
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
