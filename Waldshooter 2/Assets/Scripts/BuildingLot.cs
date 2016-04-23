using UnityEngine;
using System.Collections;

public class BuildingLot : MonoBehaviour {

    GameObject turret;
    GameObject wall;

    bool GUIOpen = false;

    void Start()
    {
        /*
         * von GameManager holen
        turret =  
        wall = 
         * 
         */ 
    }

    void OnTriggerStay(Collider other)
    {
        //Debug.Log("Bauen?");

        if (!GUIOpen && Input.GetKey(KeyCode.C))
        {
            GUIOpen = true;
            Debug.Log("Bauen");
            // baumenü öffnen
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (GUIOpen)
        {
            // baumenü schließen
            GUIOpen = false;
        }
    }

    public void buildTurret()
    {
        Instantiate(turret, transform.position, transform.rotation);
    }

    public void buildWall()
    {
        Instantiate(wall, transform.position, transform.rotation);
    }
}
