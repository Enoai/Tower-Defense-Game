using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class NodeUI : MonoBehaviour{

    public GameObject ui; // contains the upgrade/sell UI;

    private Node target; // stores the current selected node

    public Text upgradeCost;
    public Button upgradeButton;

    public Text sellAmount;

    public GameObject notEnoughMoney;

    // method used for updating/setting the target
    // It gets the build position allowing the UI to be instantly palced upon the correct location
    // then checks to see if it has been upgraded or not and set the correct informatinos buttons to be enbalabed
    // and finally sets the rangecircle + the sell/upgraded ui to be on for the selected node.
    public void SetTarget(Node _target)
    {
        target = _target; // sets target to be equal to the set target from buildmanager.

        transform.position = target.GetBuildPosition(); // sets the postion to be equal to the current build position of the object.

        if (!target.isUpgraded)// if the target isn't upgraded yet, show the upgrade button still
        {
            upgradeCost.text = "$" + target.turretBlueprint.upgradeCost; // when ever we set a new turret it sets the correct upgrade cost.
            upgradeButton.interactable = true;
        }
        else// if is already upgraded disable it and show it.
        {
            upgradeCost.text = "COMPLETE!";
            upgradeButton.interactable = false;
        }

        // if the target is upgraded, upgrade the UI to show the correct amount
        if (!upgradeButton.interactable)
        {
            sellAmount.text = "$" + target.turretBlueprint.GetUpgradedSellAmount(); // grabs the sell ammount and updates the sell cost on hte button
        }
        else
        {
            sellAmount.text = "$" + target.turretBlueprint.GetSellAmount(); // grabs the sell ammount and updates the sell cost on hte button
        }

        ui.SetActive(true); // enables the UI
        _target.rangeTriggerController(true); // enables it
    }

    // This method is used for hiding the curret UI (Upgrade/sell) but also setting the current targets range detecting to off (IF) the target is not currenlty null
    public void Hide()
    {
        if (target != null)
        {
            target.rangeTriggerController(false); // disables it
        }
       
        ui.SetActive(false);//disables the uI
    }

    // This method is used for disabling the range detector, then calling the upgrade turret functin and finally deselecting the current node. 
    //(allowing the fundermentals to update, price, can be updated etc.)
    public void Upgrade() // upgrades the turret
    {
        target.rangeTriggerController(false); // disables the old circle and stops a ---missingreferenceexception--
        target.UpgradeTurret();// upgrades the turret
        BuildManager.instance.DeselectNode(); // instantly closes menu after upgrade of selected node
    }

    // This method sells the current selected turret and instantly deselects it by removing the UI and finally sets the target to be null leaving it ready for the next target to be imputted.
    public void Sell() // sells the curret turret
    {
        BuildManager.instance.DeselectNode(); // of selected node
        target.SellTurret();
        target = null; // deletes it from the NODEUI script so it's now empty and is no longer the previous target // removes htis if have to

    }

    //simple method to which is called once moneyfader is required to happen.
    public void notEnoughMoneyFader()
    {
        StartCoroutine(notEnoughMoneyFade());
    }

    //This enumerator is used for showing the unable to build / not enough money then fade out after 2 seconds
    public IEnumerator notEnoughMoneyFade()
    {
        notEnoughMoney.SetActive(true);
        yield return new WaitForSeconds(2f);
        notEnoughMoney.SetActive(false);
    }
}
