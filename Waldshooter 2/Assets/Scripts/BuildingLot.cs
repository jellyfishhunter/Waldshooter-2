using UnityEngine;
using System.Collections;

public class BuildingLot : MonoBehaviour {
    public GameObject building = null;

    GameObject turret;
    GameObject wall;
    BuildManager bm;

    bool GUIOpen = false;

    void Start()
    {
        if(building != null)
        {
            building.transform.position = transform.position;
            building.transform.rotation = transform.rotation;
        }
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
        building = Instantiate(turret, transform.position, transform.rotation) as GameObject;
    }

    public void buildWall()
    {
        building = Instantiate(wall, transform.position, transform.rotation) as GameObject;
    }

    public void repair()
    {
        if (building.GetComponent<Wall>() != null)
        {
            building.GetComponent<Wall>().repair();
        }
        else if (building.GetComponent<Turret>() != null)
        {
            //building.GetComponent<Turret>();
        }
    }

    public int repairCosts()
    {

        return 0;
    }

    public void upgrade()
    {
        if (building.GetComponent<Wall>() != null)
        {
            building.GetComponent<Wall>().upgrade();
        }
        else if (building.GetComponent<Turret>() != null)
        {
            building.GetComponent<Turret>().upgrade();
        }
    }

    public int upgradeCosts()
    {
        if (building.GetComponent<Wall>() != null)
        {
            return building.GetComponent<Wall>().upgradeCosts;
        }
        else if (building.GetComponent<Turret>() != null)
        {
            return building.GetComponent<Turret>().upgradecosts;
        }

        return 0;
    }

    public void closeGUI()
    {
        GUIOpen = false;
    }
}
