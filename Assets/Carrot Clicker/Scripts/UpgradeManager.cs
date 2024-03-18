using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AddCarrots", 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void AddCarrots()
    {
        UpgradeSO[] upgrades = ShopManager.instance.GetUpgrades();

        if(upgrades.Length <= 1)
            return;

        double totalCarrots = 0;

        for (int i = 1; i < upgrades.Length; i++)
        {
            //Grab the amount of carrots for the upgrade
            double upgradeCarrots = upgrades[i].cpsPerLevel * ShopManager.instance.GetUpgradeLevel(i);
            totalCarrots += upgradeCarrots;
        }

        //Burada, her saniye baþý ekleyeceðimiz havuçlara ihtiyacýmýz var.

        CarrotManager.instance.AddCarrots(totalCarrots);
    }
}
