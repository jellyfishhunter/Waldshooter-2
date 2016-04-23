using UnityEngine;
using System.Collections;

public class BuildingLot : MonoBehaviour {

    GameObject turret;
    GameObject wall;
    BuildManager bm;

    bool GUIOpen = false;

    void Start()
    {
        bm = GameObject.Find("Build Manager").GetComponent<BuildManager>();
        turret = bm.turret;
        wall = bm.wall;
    }

    void OnTriggerStay(Collider other)
    {

        if (!GUIOpen && Input.GetKey(KeyCode.C))
        {
            GUIOpen = true;

            bm.OpenMenu(this);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (GUIOpen)
        {
            bm.CloseMenu(this);

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

    public void repair()
    {

    }

    public void upgrade()
    {

    }

    public void closeGUI()
    {
        GUIOpen = false;
    }
}
