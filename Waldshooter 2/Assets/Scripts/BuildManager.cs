using UnityEngine;
using System.Collections;

//TODO Money n shit
public class BuildManager : MonoBehaviour {

    public GameObject turret;
    public GameObject wall;
    public GameObject buildMenu;

    BuildingLot active = null;
    Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update()
    {
        if (active != null)
        {
            //Cursor.visible = true;
        }
    }

    public void OpenMenu(BuildingLot lot)
    {
        if (active == null)
        {
            buildMenu.SetActive(true);
            active = lot;
        }
    }

    public void CloseMenu(BuildingLot lot)
    {
        if (active == lot)
        {
            buildMenu.SetActive(false);
            active = null;
        }
    }

    public void buildTurret()
    {
        active.buildTurret();
    }

    public void buildWall()
    {
        Debug.Log("build wall");
        active.buildWall();

    }

    // TODO
    public void Repair()
    {
        active.repair();
    }

    public void Upgrade()
    {
        active.upgrade();
    }

    public void Cancel()
    {
        Debug.Log("build cancel");

        active.closeGUI();
        buildMenu.SetActive(false);
        active = null;
    }
}
