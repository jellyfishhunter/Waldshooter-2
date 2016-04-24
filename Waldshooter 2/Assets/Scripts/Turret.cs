using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class Turret : MonoBehaviour {


	public List<Transform> enemysInTrigger;  

	private Transform targetTransform; 
	private enum States { attackEnemy, onHold};

	float timeUntilnextShot = 0f; 
	float attackIntervall = 0.5f; 
	float range = 20f; 

	int hp;
    public int maxHp = 100;
	int level = 1; 
	public int costs = 20; 
	public int upgradecosts = 15; 

	private States myState;

    public Transform bombSpawn;
	public Transform Target;
	public float firingAngle = 45.0f;
	public float gravity = 9.8f;

	public GameObject Projectile;      
	private Transform myTransform;

    bool shot = false;

	// Use this for initialization
	void Start () {
		myTransform = transform;
        hp = maxHp;
    }

    // Update is called once per frame
    void Update () {
        //Debug.DrawRay(Target.transform.position, Vector3.up);
        enemyCheck();
        aimAndShoot();

    }

    void enemyCheck()
    {
        while(enemysInTrigger.Count != 0)
        {
            if(enemysInTrigger[0] != null)
            {
                Target = enemysInTrigger[0];
                return;
            }
            else
            {
                enemysInTrigger.RemoveAt(0);
            }
        }
    }

    void aimAndShoot()
    {
        if (Target == null)
            return;

        targetTransform = Target.transform;
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

    void shoot()
    {
        GameObject bulletInstance = Instantiate(Projectile, bombSpawn.position, transform.rotation) as GameObject;
        Bomb bulletScript = bulletInstance.GetComponent<Bomb>();
        bulletScript.range = range;
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

    
    void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Enemy") {
            Debug.Log("Enemy detected");
            enemysInTrigger.Add(other.transform);
		}

			
	}

	void OnTriggerExit(Collider other) {
		if (other.gameObject.tag == "Enemy") {
            enemysInTrigger.Remove(other.transform);
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

    public void repair()
    {

    }

    public void upgrade()
    {
        level++;
        maxHp = (int)(maxHp * 1.5f);
        hp = maxHp;
        upgradecosts = (int)(upgradecosts * 1.5f);
        attackIntervall *= 0.8f;
        range *= 1.2f;
    }

}
