using UnityEngine;
using System.Collections;

public class BaseTree : MonoBehaviour {

    public int hp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void hit(GameObject bullet)
    {
        hp -= bullet.GetComponent<Bullet>().hitValue;

		if (hp <= 0) {
			Debug.Log("Base destroyed");
			GameObject gameManager = GameObject.Find ("Game Manager"); 
			gameManager.SendMessage ("GameOver"); 
		}

    }
}
