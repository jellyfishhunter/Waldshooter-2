using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {
    int level = 1;
    int hp;
    public int maxHp = 200;
    public int costs = 10;
    public int upgradeCosts = 8;

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
        level++;
        maxHp = (int)(maxHp * 2f);
        hp = maxHp;
        upgradeCosts = (int)(upgradeCosts * 1.5f);
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
