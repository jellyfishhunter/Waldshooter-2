using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

    void ButtonActivation()
    {
        GameObject turretButton = GameObject.Find("Button Turret");
        GameObject wallButton = GameObject.Find("Button Wall");
        GameObject upgradeButton = GameObject.Find("Button Upgrade");
        GameObject cancelButton = GameObject.Find("Button Cancel");

        if(active.building == null)
        {
           // turretButton.GetComponent<Button>().interactable = player.Money >= turret.GetComponent<Turret>().costs;
            wallButton.GetComponent<Button>().interactable = player.Money >= wall.GetComponent<Wall>().costs;
            upgradeButton.GetComponent<Button>().interactable = false;
        }
        else
        {
          //  turretButton.GetComponent<Button>().interactable = false;
            wallButton.GetComponent<Button>().interactable = false;
            upgradeButton.GetComponent<Button>().interactable = player.Money >= active.upgradeCosts();
        }
    }

    void TextUpdate()
    {
        Text turretText = GameObject.Find("Text Turret").GetComponent<Text>();
        Text wallText = GameObject.Find("Text Wall").GetComponent<Text>();
        Text upgradeText = GameObject.Find("Text Upgrade").GetComponent<Text>();

        //turretText.text = "" + turret.GetComponent<Turret>().costs;
        wallText.text = "" + wall.GetComponent<Wall>().costs;
        upgradeText.text = "" + active.upgradeCosts();
    }

    public void OpenMenu(BuildingLot lot)
    {
        if (active == null)
        {
            buildMenu.SetActive(true);
            active = lot;
            ButtonActivation();
            TextUpdate();
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
        Cancel();
    }

    public void buildWall()
    {
        if (active.building == null && player.Money >= wall.GetComponent<Wall>().costs)
        {
            active.buildWall();
            player.Money -= wall.GetComponent<Wall>().costs;
        }
        Cancel();
    }

    // TODO
    public void Repair()
    {
        if (active.building != null && player.Money >= active.repairCosts())
        {
            active.repair();
            player.Money -= active.repairCosts();
        }
        Cancel();
    }

    public void Upgrade()
    {
        if (active.building != null && player.Money >= active.upgradeCosts())
        {
            active.upgrade();
            player.Money -= active.upgradeCosts();
        }
        Cancel();
    }

    public void Cancel()
    {
        Debug.Log("build cancel");

        active.closeGUI();
        buildMenu.SetActive(false);
        active = null;
    }
}
