using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is used for checking if a player is able to build in a location and if they have the money required for the correct object to buold there.
public class BuildManager : MonoBehaviour {

    public static BuildManager instance; // used for storing a refernce to itself

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("more than one buildmanger in scene!");
            return;
        }

        instance = this; // this build manager right here is going to be put into the refernce to instance, allowing a refernce to it to happen
    }

    public GameObject buildEffect;
    public GameObject sellEffect;

    private TurretBlueprint turretToBuild; // variable that stores what turret will be set.
    private Node selectedNode; // variable that contains the current selected node.

    public NodeUI nodeUI;

    public GameObject unabletoBuild;

    public bool CanBuild { get { return turretToBuild != null; } } // variable can never be set, if turret to build returns not equal to null it'll return true, else it'll return false
    public bool HasMoney { get { return PlayerStats.Money >= turretToBuild.cost; } } // if we have enough money, return true.
    
    public void SelectNode(Node node)
    {
        if (selectedNode == node) // if the current clicked node is already clicked, deselect it
        {
            DeselectNode();
            return;
        }

        if (selectedNode != null) // if the previous node is not null, turn off the range detector
        {
            selectedNode.rangeTriggerController(false); // disables the previous selectednode's range circle.
        }

        if (turretToBuild != null) // if you have currently got a turret to build and click a place with a turret alread yon show the error message unable to build here
        {
            StartCoroutine(unabletoBuildFade());
        }

        selectedNode = node; // set the current selected node to the vairable Node node.
        turretToBuild = null; // removes the current selected turret to build when clicking on a node with a turret already on , reneable this if you want this. 

        nodeUI.SetTarget(node); // sets the current node and makes the location update.
    }

    public void DeselectNode() // deslects the current node and hides
    {
        selectedNode = null; // deselects the current node
        nodeUI.Hide(); // hides it
    }

    public void SelectTurretToBuild(TurretBlueprint turret) //used for selecting turrets to build
    {
        turretToBuild = turret;
        DeselectNode();
    }

    // simple method to which calles the not enough money fader in the nodeUI allowing it show a visual representive of not having enough money
    public void notEnoughMoney()
    {
        nodeUI.notEnoughMoneyFader();
    }

    public TurretBlueprint GetTurretToBuild()
    {
        return turretToBuild;
    }

    //This enumerator is used for showing the unable to build / not enough money then fade out after 2 seconds
    IEnumerator unabletoBuildFade()
    {
        unabletoBuild.SetActive(true);
        yield return new WaitForSeconds(2f);
        unabletoBuild.SetActive(false);
    }

}
