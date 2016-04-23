using UnityEngine;
using System.Collections;

// TODO AI verhalten überdenken
public class Enemy : MonoBehaviour {

    public int hp;
    public float range;
    public GameObject dropObject;
    public GameObject bullet;
    public float attackIntervall;

    GameObject player;
    Transform targetTransform;
    NavMeshAgent agent;
    float timeUntilnextShot = 0;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        targetTransform = chooseTarget();
        moveToTarget();
	}

    void moveToTarget()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.destination = targetTransform.position;
        agent.stoppingDistance = range;
        agent.Resume();
    }

    public void initialize()
    {

    }
	
	// Update is called once per frame
	void Update () {
        targetTransform = chooseTarget();
        aimAndShoot();
    }

    bool inRangeOf(GameObject other)
    {
        return (Vector3.Distance(other.transform.position, transform.position) <= range);
    }

    Transform chooseTarget()
    {
        if (inRangeOf(player))
            return player.transform;

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
        int lootCount = Random.Range(0, 5);

        for (int i = 0; i<lootCount; i++)
        {
            Instantiate(dropObject, transform.position, Random.rotation);
        }

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
