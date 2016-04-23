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

    void ButtonActivation()
    {
        GameObject turretButton = GameObject.Find("Button Turret");
        GameObject wallButton = GameObject.Find("Button Wall");
        GameObject upgradeButton = GameObject.Find("Button Upgrade");
        GameObject cancelButton = GameObject.Find("Button Cancel");
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
        if(active.building == null && player.Money >= turret.GetComponent<Turret>().costs)
        {
            active.buildTurret();
            player.Money -= turret.GetComponent<Turret>().costs;
        }
    }

    public void buildWall()
    {
        if (active.building == null && player.Money >= wall.GetComponent<Wall>().costs)
        {
            active.buildWall();
            player.Money -= wall.GetComponent<Wall>().costs;
        }
    }

    // TODO
    public void Repair()
    {
        if (active.building != null && player.Money >= active.repairCosts())
        {
            active.repair();
            player.Money -= active.repairCosts();
        }
    }

    public void Upgrade()
    {
        if (active.building != null && player.Money >= active.upgradeCosts())
        {
            active.upgrade();
            player.Money -= active.upgradeCosts();
        }
    }

    public void Cancel()
    {
        Debug.Log("build cancel");

        active.closeGUI();
        buildMenu.SetActive(false);
        active = null;
    }
}
