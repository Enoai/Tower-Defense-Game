using UnityEngine;
using UnityEngine.EventSystems;

//This class is used for detecting if a player is clicking a node and allowing it to be buildable or not.
//This Class is used for storing the current turret placed upon this node.
//This Class checks/uses the detection gameobject to which controls to whether it's on or off currently depending if the object is clicked upon.
public class Node : MonoBehaviour {

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 positionOffset;

    [HideInInspector]
    public GameObject turret; // stores the current turret on this node., but also allows preplaced turrets on node.

    public GameObject detection;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;
    [HideInInspector]
    public bool isUpgraded = false;

    private Renderer rend;
    private Color startColor; // used to store the orignal colour of a object

    BuildManager buildManager;

    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color; // instantly set the default colour of the base object to this.

        buildManager = BuildManager.instance;
    }

    public Vector3 GetBuildPosition () // returns the current coirdinates of the build position.
    {
        return transform.position + positionOffset;
    }

    void OnMouseDown() // when mouse pressed down over object
    {
        if (EventSystem.current.IsPointerOverGameObject())//checks to see if the cursors is over a event system object, if so, don't let it pass.
        {
            return;
        }
        if (turret != null) // if node is occupied, don't allow to build there and move the ui for selected to this node.
        {
            Debug.Log("Unable to build here, object already here - TODO:Display on screen.");
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild) // if turret is not selected, do not show that it is allowed to place
        {
            return;
        }

        //Build a Turret
        BuildTurret(buildManager.GetTurretToBuild()); // creates the turret and grabs the turret from the buildmanager.
    }

    void BuildTurret(TurretBlueprint blueprint) // builds the turret / grabs the clicked turret from the screen.
    {
        if (PlayerStats.Money < blueprint.cost)
        {
            buildManager.notEnoughMoney();
            return;
        }

        PlayerStats.Money -= blueprint.cost;

        //Build a Turret
        GameObject _turret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity); // gets the turret  then position , but not rotation.
        turret = _turret;

        detection = GameObject.Find("Detection"); // sets the detection variable to be the nearest decetion variable on the scene.

        turretBlueprint = blueprint; // sets the blueprint to the node allowing to be sold and upgraded

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity); // sets it into a gameobject so we can remove it later, stops clutter in heiarchy
        Destroy(effect, 5f);
        detection.SetActive(false);//disables it so it can set the next one up
        Debug.Log("Turret Built!");
    }

    public void UpgradeTurret()
    {
        // if the turret has the tag moneyturret, remove the unpgraded amount of money
        

        if (PlayerStats.Money < turretBlueprint.upgradeCost)
        {
            buildManager.notEnoughMoney();
            return;
        }

        if (turret.tag == "MoneyTurret")
        {
            PlayerStats.moneyGenerationAmount -= 10;
        }

        PlayerStats.Money -= turretBlueprint.upgradeCost;

        Destroy(turret); // destroys the current turret on the board.


        //Build a new/upgraded turret
        GameObject _turret = (GameObject)Instantiate(turretBlueprint.upgradedPrefab, GetBuildPosition(), Quaternion.identity); // gets the turret  then position , but not rotation.
        turret = _turret;

        detection = GameObject.Find("Detection");

        GameObject effect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity); // sets it into a gameobject so we can remove it later, stops clutter in heiarchy
        Destroy(effect, 5f);

        isUpgraded = true;
        detection.SetActive(false); // after upgrading the turret, disable its range circle
        Debug.Log("Turret upgraded!");
    }

    public void SellTurret() // method used for selling the turret on the current node.
    {
        if (turret.tag == "MoneyTurret")
        {
            if (isUpgraded == true)
            {
                PlayerStats.moneyGenerationAmount -= 20; // changed this when ever you change the money the money turrets bring in *UPGARDED*
            }
            else
            {
                PlayerStats.moneyGenerationAmount -= 10; // changed this when ever you change the money the money turrets bring in *UPGARDED*
            }
            
        }


        if (isUpgraded) // if the turret is upgraded upgrade the sell price
        {
            PlayerStats.Money += turretBlueprint.GetUpgradedSellAmount(); //TODO - Make a variable to which counts the upgrades for the sell price
        }
        else
        {
            PlayerStats.Money += turretBlueprint.GetSellAmount(); //TODO - Make a variable to which counts the upgrades for the sell price
        } 

        //Turret effect
        GameObject effect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity); // sets it into a gameobject so we can remove it later, stops clutter in heiarchy
        Destroy(effect, 5f);


        detection = null;
        isUpgraded = false; // allows the turret to be upgraded again.
        Destroy(turret); // removes the turret
        turretBlueprint = null; // makes the nodes blueprint null
    }
    void OnMouseEnter () // every time the mouse enters the area of the collider to this object
    {
        if (EventSystem.current.IsPointerOverGameObject())//checks to see if the cursors is over a event system object, if so, don't let it pass.
        {
            return;
        }

        if (!buildManager.CanBuild) // if a turret is not selected, don't allow hovering over squares
        {
            return; // stops the method running any code below
        }
        
        if (buildManager.HasMoney) // if the player has enough money, do this.
        {
            rend.material.color = hoverColor;
        }
        else
        {
            rend.material.color = notEnoughMoneyColor;
        }

    }

    void OnMouseExit () // when exit set back to default colour
    {    
        rend.material.color = startColor;
    }

// controls
    public void rangeTriggerController(bool onOff)
    {
        detection.SetActive(onOff);
    }
}
