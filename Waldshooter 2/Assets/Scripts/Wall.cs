using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
    int level = 1;
    int hp;
    public int maxHp = 200;
    public int costs = 10;
    public int upgradeCosts = 15;

	// Use this for initialization
	void Start () {
        hp = maxHp;
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
        hp -= bullet.GetComponent<Bullet>().hitValue;
        if (hp <= 0)
        {
            // destroy
        }
    }
}
