using UnityEngine;
using System.Collections;

// TODO was passiert, wenn der spieler stirbt?
public class Player : MonoBehaviour
{

    private enum States { running, shooting };
    private States myState;

    public float RunSpeed = 10f;
    public float ShootingSpeed = 4f;
    private float speed;
    private float move = 0f;
    public float tilt;

    public int Health;
    public int Money;
    Rigidbody PlayerRigidbody;

    public float fireRate;
    private float nextFire;

    public GameObject PlayerBullet;
    public float distance = 10.0f;

    public Transform bulletSpawn;
    public Transform gunTrail;

    void Start()
    {
        myState = States.running;
        PlayerRigidbody = this.GetComponent<Rigidbody>();
        gunTrail.gameObject.SetActive(false);
    }

    void LookAtMouse()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        // This will be the point that the object must look towards to be looking at the mouse.
        // Raycasting to a Plane object only gives us a distance, so we'll have to take the distance,
        //   then find the point along that ray that meets that distance.  This will be the point
        //   to look at.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 20 * Time.deltaTime);
        }
    }



    void FixedUpdate()
    {

        if (Input.GetButton("Fire2"))
        {
            myState = States.shooting;
        }
        else
        {
            myState = States.running;
        }


        if (myState == States.running)
        {
            speed = RunSpeed;
            gunTrail.gameObject.SetActive(false);
            ViewToDirection();
            Camera.main.gameObject.SendMessage("MoveIn");
        }

        if (myState == States.shooting)
        {
            speed = ShootingSpeed;
            Shoot();
            gunTrail.gameObject.SetActive(true);
            Camera.main.gameObject.SendMessage("MoveOut");
        }
        Move();
    }

    void Move()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        PlayerRigidbody.position += move * speed * Time.deltaTime;
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal").ToString());
        //Debug.Log("Vertical: " + Input.GetAxis("Vertical").ToString());

        //transform.rotation = Quaternion.LookRotation(move);

    }

    void ViewToDirection()
    {
        var move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        face(PlayerRigidbody.position + move);

    }

    void Shoot()
    {
        Vector3 position = aim();
        face(position);

        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            Debug.Log("Shoot-Funktion");
            nextFire = Time.time + fireRate;

            /*
            var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            position = Camera.main.ScreenToWorldPoint(position);
<<<<<<< Updated upstream
            
			var go = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
           // go.transform.LookAt(position);
=======

			var go = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
           // go.transform.LookAt(position);

>>>>>>> Stashed changes
            */

            var go = Instantiate(PlayerBullet, bulletSpawn.position, bulletSpawn.rotation) as GameObject;
            //go.transform.LookAt(position);
            Debug.Log(position);
            //go.GetComponent<Rigidbody>().AddForce(go.transform.forward * 1000);

        }
    }

    Vector3 aim()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit);
        return hit.point;
    }

    public void hit(GameObject bullet)
    {
        Debug.Log("Player Hit");

        Health -= bullet.GetComponent<Bullet>().hitValue;
        if (Health <= 0)
        {
            Debug.Log("Player Dead");
            //die();
        }
    }

    public void collectLoot(Loot loot)
    {
        Debug.Log("Got Loot!");
        Money += loot.value;
    }

    public void face(Vector3 t)
    {
        Vector3 direction = (new Vector3(t.x, PlayerRigidbody.position.y, t.z) - PlayerRigidbody.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            PlayerRigidbody.rotation = Quaternion.Slerp(PlayerRigidbody.rotation, lookRotation, Time.deltaTime * 10f);
        }
    }
}