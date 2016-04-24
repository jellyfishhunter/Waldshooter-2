using UnityEngine;
using System.Collections;
using System.Collections.Generic; 

public class ShipManager : MonoBehaviour {
	
	public List<Transform> lanes;
	public static int energy = 50; 
	int laneIndex = 1; 
	public GameObject engine; 
	bool enegeryDownerActive = false; 
	GameObject spaceShip; 
	public static bool gameOver = false; 
	public GameObject explosion; 
	public static float gameTime = 1; 

	// Use this for initialization
	void Start () {
		spaceShip = GameObject.FindGameObjectWithTag ("Player"); 
		gameOver = false; 
		energy = 50; 
	
	}

	IEnumerator RestartTimer(){
		yield return new WaitForSeconds (2); 
		Application.LoadLevel (0); 

	}

	IEnumerator EnergyDowner(){
		yield return new WaitForSeconds (1.0f/gameTime); 
		energy -= 1; 
		enegeryDownerActive = false; 

		}

	// Update is called once per frame
	void Update () {
		if (energy <= 0 && !gameOver) {
			gameOver = true; 
			engine.SetActive (false); 
			GameObject myParticleSystem = (GameObject)Instantiate (explosion, spaceShip.transform.position, Quaternion.identity); 
			Destroy (spaceShip.gameObject); 
			StartCoroutine (RestartTimer ()); 

		}
		if (!gameOver) {

			if(!enegeryDownerActive){
					StartCoroutine (EnergyDowner()); 
					enegeryDownerActive = true; 
			}


		if (Input.GetKeyDown (KeyCode.RightArrow)) {
			if (laneIndex < lanes.Count-1) {
				spaceShip.GetComponent<Animator> ().SetInteger ("turnDirection", 0); 

				laneIndex++; 
			}
		}
		if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (laneIndex > 0) {
				spaceShip.GetComponent<Animator> ().SetInteger ("turnDirection", 0); 
				laneIndex--; 
			}
		}

		//spaceShip.transform.position = lanes [laneIndex].position; 
		Vector2 targetPosition = new Vector2(lanes[laneIndex].position.x, lanes[laneIndex].position.y);
		Vector2 shipPosition = new Vector2 (spaceShip.transform.position.x, spaceShip.transform.position.y); 
		Vector2 distanceVector = shipPosition - targetPosition; 
		float distanceFloat = distanceVector.sqrMagnitude; 

		if (distanceFloat < 0.4f) {
			spaceShip.GetComponent<Animator> ().SetInteger ("turnDirection", 0); 

		}


			spaceShip.transform.position = Vector2.Lerp(spaceShip.transform.position, new Vector2(lanes[laneIndex].position.x, lanes[laneIndex].position.y), 10*Time.deltaTime*gameTime);
	}

	}
		
}
