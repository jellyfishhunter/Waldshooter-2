using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private enum States{gameover, fightloop, buildloop, start};
	private States myState;

	[Header("Loop Timers")]
	public float fightLoopTime = 5.0f; 
	public float buildLooptime = 5.0f; 

	[Header("Enemy related Stuff (Lists)")]
	public List<Transform> enemySpawnPoints; 
	public List<GameObject> enemys; 

	[Header("Audio Files")]
	public AudioClip fightLoopAudio; 
	public AudioClip buildLoopAudio; 

	bool isSpawningEnemys = false; 
	bool loopTimerActive = false; 
	int waveSize = 0; 

	void Start () {
		myState = States.fightloop; 
	}
	
	void Update () {
		
		//GAMESTART
		if (myState == States.start) {
			Debug.Log ("Game started"); 
			myState = States.fightloop; 
		}

		//BUILDLOOP
		if (myState == States.buildloop) {
			waveSize = 0; 
			Cursor.visible = true;

			if (!loopTimerActive) {
				StartCoroutine (LoopTimer (buildLooptime, myState, States.fightloop)); 
				loopTimerActive = true; 
			}		
		}

		//FIGHTLOOP
		if (myState == States.fightloop) {
			Cursor.visible = false;

			if(!isSpawningEnemys) {
				StartCoroutine(SpawnEnemys (enemys[0], enemySpawnPoints[0], 0.01f)); 
				isSpawningEnemys = true; 
				waveSize++; 
			}

			if (waveSize >= 10) {
				myState = States.buildloop; 
			}
		}

		//GAMEOVER
		if (myState == States.gameover) {
			Debug.Log ("Game Over"); 
		}
	}
		
	IEnumerator SpawnEnemys(GameObject enemy, Transform spawnPosition, float waitTime){
		GameObject myEnemy = (GameObject)Instantiate (enemy, spawnPosition.position, Quaternion.identity); 
		Debug.Log ("Enemy spawned, wave: "+waveSize); 
		yield return new WaitForSeconds (0.5f); 
		isSpawningEnemys = false;
	}

	IEnumerator LoopTimer(float myTime, States initState, States nextState){
		yield return new WaitForSeconds (myTime); 
		if (initState == myState) {
			myState = nextState; 
		}
		Debug.Log ("Next State: " + nextState); 
		loopTimerActive = false; 
	}

	IEnumerator  RestartGame(){
		Debug.Log ("Restart in 2 Seconds.."); 
		yield return new WaitForSeconds (1); 
		SceneManager.LoadScene (0); 

	}

	public void GameOver(){
		myState = States.gameover; 
		Debug.Log ("Gameover"); 
	}
}
