using UnityEngine;
using System.Collections;

public class OnLevelLoaded : MonoBehaviour {

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            print("Level " + level.ToString() + " loaded");
            Time.timeScale = 1;
        }
    }
}
