using UnityEngine;
using System.Collections;

// TODO AI verhalten überdenken
public class Enemy : MonoBehaviour {

    private enum States { attackTree, chasePlayer, attackPlayer, attackObstacle };
    private States behaviourState;

    public int hp;
    public float range;
    public GameObject dropObject;
    public GameObject bullet;
    public float attackIntervall;


	private Vector3 previousPosition;

    GameObject player;
    GameObject tree;
    Transform targetTransform;
    NavMeshAgent agent;
    float timeUntilnextShot = 0;
    float timeUntilStopChasingPlayer = 0;

	public GameObject animationObject; 
	Animator enemyAnimator; 

	public GameObject bloodSystem; 

    GameObject GameManager;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        tree = GameObject.FindGameObjectWithTag("Base Tree");
        GameManager = GameObject.Find("Game Manager");
        agent = GetComponent<NavMeshAgent>();
        behaviourState = States.attackTree;
        targetTransform = tree.transform;
        moveToTarget();


		enemyAnimator = animationObject.GetComponent<Animator> ();
	}

    void moveToTarget()
    {
        agent.destination = targetTransform.position;
        agent.stoppingDistance = range;
        agent.Resume();
    }
	
	// Update is called once per frame
	void Update () {
        checkBehaviour();
        aimAndShoot();

		//noch ordentlich einbauen? prüfen ob er sich bwergt und dann animation abspielen. 
		if (transform.position.x < previousPosition.x) {
			enemyAnimator.SetInteger ("movingState", 1); 
			Vector3 posScale = new Vector3 (-1, 1, 1); 
			animationObject.transform.localScale = posScale; 
		} else if (transform.position.x > previousPosition.x) {
			enemyAnimator.SetInteger ("movingState", 1); 
			Vector3 posScale = new Vector3 (1, 1, 1); 
			animationObject.transform.localScale = posScale; 
		} else {
			enemyAnimator.SetInteger ("movingState", 0); 
		}
		previousPosition = transform.position;
			
	
    }

    void checkBehaviour()
    {
        switch (behaviourState)
        {
            case States.attackTree:
                if (inRangeOf(player))
                {
                    targetTransform = player.transform;
                    behaviourState = States.attackPlayer;
                    moveToTarget();
                    return;
                }

                if(!inRangeOf(tree))
                {
                    GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");

                    foreach (GameObject building in buildings)
                    {
                        if (inRangeOf(building))
                        {
                            targetTransform = building.transform;
                            behaviourState = States.attackObstacle;
                            return;
                        }
                    }
                }

                break;

            case States.chasePlayer:
                moveToTarget();

                if (inRangeOf(player))
                {
                    targetTransform = player.transform;
                    behaviourState = States.attackPlayer;
                    return;
                }
                else if(timeUntilStopChasingPlayer<=0)
                {
                    targetTransform = tree.transform;
                    behaviourState = States.attackTree;
                    moveToTarget();
                    return;
                }
                timeUntilStopChasingPlayer -= Time.deltaTime;
                break;

            case States.attackPlayer:
                if (!inRangeOf(player))
                {
                    targetTransform = player.transform;
                    behaviourState = States.chasePlayer;
                    moveToTarget();

                    timeUntilStopChasingPlayer = Random.Range(1f, 5f);
                    return;
                }
                break;

            case States.attackObstacle: // läuft hier weiter richtung baum
                if (inRangeOf(tree) || targetTransform == null)
                {
                    targetTransform = tree.transform;
                    behaviourState = States.attackTree;
                    moveToTarget();
                    return;
                }
                break;
        }
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
        if(bullet.GetComponent<Bullet>() != null)
            hp -= bullet.GetComponent<Bullet>().hitValue;
        else
            hp -= bullet.GetComponent<Bomb>().hitValue;

        if (hp <= 0)
        {
            die();
        }
    }

    // TODO: enemy counter etc
    void die()
    {
		GameObject myBloodSytem = (GameObject)Instantiate (bloodSystem, transform.position, Quaternion.identity);
		Destroy (myBloodSytem, 2); 

        GameManager.GetComponent<GameManager>().livingEnemies--;
        player.GetComponent<Player>().Kills++;
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
