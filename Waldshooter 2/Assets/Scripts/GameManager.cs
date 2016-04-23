using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    //public static List<Transform> spawnedEnemies = new List<Transform>();

    private enum States{gameover, fightloop, buildloop, start};
	private States myState;

	[Header("Loop Timers")]
	public float fightLoopTime = 5.0f; 
	public float buildLooptime = 100.0f; 

	[Header("Enemy related Stuff (Lists)")]
	public List<Transform> enemySpawnPoints; 
	public List<GameObject> enemys; 

	[Header("Audio Files")]
	public AudioClip fightLoopAudio; 
	public AudioClip buildLoopAudio; 

	bool isSpawningEnemys = false; 
	bool loopTimerActive = false; 
	int waveSize = 10;
    public int livingEnemies;
    int spawnedEnemies = 0;

    public GameObject GameOverMenu;

	void Start () {
		myState = States.buildloop;
        SetAudio(myState);
	}
	
	void Update () {

        //Debug.Log(myState);

		//GAMESTART
		if (myState == States.start) {
			Debug.Log ("Game started"); 
			myState = States.fightloop;
        }

		//BUILDLOOP
		if (myState == States.buildloop) {
          //  Debug.Log("bin in buildloop");
            //waveSize = 0;
            spawnedEnemies = 0;
			Cursor.visible = true;

			if (!loopTimerActive) {
				StartCoroutine (LoopTimer (buildLooptime, myState, States.fightloop)); 
				loopTimerActive = true;
            }
        }

		//FIGHTLOOP
		if (myState == States.fightloop) {
			//Cursor.visible = false;


			if(!isSpawningEnemys && spawnedEnemies < waveSize) {
				StartCoroutine(SpawnEnemys (enemys[0], enemySpawnPoints[0], 0.01f));
                //Debug.Log("livingEnemies: " + livingEnemies.ToString());
                isSpawningEnemys = true; 
				//waveSize++; 
			}

			//if (waveSize >= 10) {
            if(livingEnemies == 0 && spawnedEnemies == waveSize) {
                //Debug.Log("spawnedEnemies = " + spawnedEnemies.ToString());
                //Debug.Log("waveSize = " + waveSize.ToString());
				myState = States.buildloop;
                Debug.Log("wechsle zu buildloop");
                SetAudio(myState);
			}
		}

		//GAMEOVER
		if (myState == States.gameover) {
			//Debug.Log ("Game Over");
            Time.timeScale = 0;
            GameOverMenu.SetActive(true);
            GameObject.Find("Money_Value").GetComponent<Text>().text = GameObject.Find("Player").GetComponent<Player>().Money.ToString();
            GameObject.Find("Kills_Value").GetComponent<Text>().text = GameObject.Find("Player").GetComponent<Player>().Kills.ToString();
            Cursor.visible = true;
            this.GetComponent<AudioSource>().Stop();
        }
	}
		
	IEnumerator SpawnEnemys(GameObject enemy, Transform spawnPosition, float waitTime){
		GameObject myEnemy = (GameObject)Instantiate (enemy, spawnPosition.position, Quaternion.identity);
        livingEnemies++;
        spawnedEnemies++;

        //Debug.Log("livingEnemies: " + livingEnemies.ToString());

        Debug.Log ("Enemy spawned, wave: "+waveSize); 
		yield return new WaitForSeconds (0.5f); 
		isSpawningEnemys = false;
	}

	IEnumerator LoopTimer(float myTime, States initState, States nextState){
		yield return new WaitForSeconds (myTime); 
		if (initState == myState) {
			myState = nextState;
            SetAudio(myState);
            Debug.Log("wechsle zu " + myState);
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

    private void SetAudio(States myState)
    {
        switch (myState)
        {
            case States.start:
                Debug.Log("trying to play start music");
                break;
            case States.fightloop:
                Debug.Log("trying to play fightloopaudio");
                this.GetComponent<AudioSource>().clip = fightLoopAudio;
                this.GetComponent<AudioSource>().Play();
                break;
            case States.buildloop:
                Debug.Log("trying to play buildloopaudio");
                this.GetComponent<AudioSource>().clip = buildLoopAudio;
                this.GetComponent<AudioSource>().Play();
                break;
            default:
                Debug.Log("Default case");
                break;
        }

    }

	public void LoadScene(int sceneToLoad){
		Application.LoadLevel (sceneToLoad);
	}

	public void ExitGame(){
		Application.Quit ();
	}

	void OnLevelWasLoaded(int level)
	{
		if (level == 1)
		{
			print("Level " + level.ToString() + " loaded");
			Time.timeScale = 1;
		}
	}
}
