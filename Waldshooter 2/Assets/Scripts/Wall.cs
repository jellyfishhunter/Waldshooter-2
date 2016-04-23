using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
    int level = 1;
    int currentHealth;
    public int maxHealth = 200;
    public int cost = 10;
    public int upgradeCosts = 15;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void repair()
    {

    }

    public void upgrade()
    {

    }

    public void hit(GameObject bullet)
    {
        currentHealth -= bullet.GetComponent<Bullet>().hitValue;
        if (currentHealth <= 0)
        {
            // destroy
        }
    }
}
