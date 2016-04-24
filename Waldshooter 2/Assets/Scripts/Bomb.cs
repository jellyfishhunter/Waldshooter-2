using UnityEngine;
using System.Collections;

// TODO die kugeln kollidieren miteinander :(
public class Bomb : MonoBehaviour {

	public int hitValue = 50;
    public float explosionForce;

    public float range;
    public float speed;
    float timeToLive;

    public GameObject particleSystem; 

	// Use this for initialization
	void Start () {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        timeToLive = (range / speed) + 0.1f;
    }
	
	// Update is called once per frame
	void Update () {
    }



	void OnCollisionEnter(Collision col)
    {
		ContactPoint contact = col.contacts[0];
		GameObject explosion = (GameObject)Instantiate (particleSystem, contact.point, Quaternion.identity);
		Destroy (explosion, 1); 
		Destroy (this.gameObject); 
    }

	void OnTriggerEnter(Collider other){
		GameObject target = other.gameObject; 
		if (target.tag == "Enemy") {
			target.GetComponent<Enemy>().hit(gameObject);
		}
	}



    
}
