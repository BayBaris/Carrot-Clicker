using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    [Header("Elements")]
    [SerializeField] private UpgradeButton upgradeButton;
    [SerializeField] private Transform upgradeButtonPanel;

    [Header("Data")]
    [SerializeField] private UpgradeSO[] upgrades;

    [Header("Actions")]
    public static Action<int> onUpgradePurchased;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnButtons();
    }

    public void SpawnButtons()
    {
        for (int i = 0; i < upgrades.Length; i++)
        {
            SpawnButton(i);
        }
    }

    private void SpawnButton(int index)
    {
        UpgradeButton upgradeButtonInstance = Instantiate(upgradeButton, upgradeButtonPanel);

        UpgradeSO upgrade = upgrades[index];

        int upgradeLevel = GetUpgradeLevel(index);

        Sprite icon = upgrade.icon;
        string title = upgrade.title;
        string subtitle = string.Format("lvl{0} (+{1} CPS)", upgradeLevel, upgrade.cpsPerLevel);
        string price = upgrade.GetPrice(upgradeLevel).ToString("F0");

        upgradeButtonInstance.Configure(icon, title, subtitle, price);

        upgradeButtonInstance.GetButton().onClick.AddListener(() => UpgradeButtonClickedCallback(index));
    }

    private void UpgradeButtonClickedCallback(int upgradeIndex)
    {
        if (CarrotManager.instance.TryPurchase(GetUpgradePrice(upgradeIndex)))
            IncreaseUpgradeLevel(upgradeIndex);
        else
            Debug.Log("You're too poor for the upgrade!");
    }

    private void IncreaseUpgradeLevel(int upgradeIndex)
    {   
        int currentUpgradeLevel = GetUpgradeLevel(upgradeIndex);
        currentUpgradeLevel++;
        //Save upgrade level..
        SaveUpgradeLevel(upgradeIndex, currentUpgradeLevel);

        UpdateVisuals(upgradeIndex);

        onUpgradePurchased?.Invoke(upgradeIndex);
    }

    public int GetUpgradeLevel(int upgradeIndex)
    {
        return PlayerPrefs.GetInt("Upgrade" + upgradeIndex);
    }

    private void SaveUpgradeLevel(int upgradeIndex, int upgradeLevel)
    {
        PlayerPrefs.SetInt("Upgrade" + upgradeIndex, upgradeLevel);
    }
    private void UpdateVisuals(int upgradeIndex)
    {
        UpgradeButton upgradeButton = upgradeButtonPanel.GetChild(upgradeIndex).GetComponent<UpgradeButton>();

        UpgradeSO upgrade = upgrades[upgradeIndex];

        int upgradeLevel = GetUpgradeLevel(upgradeIndex);
        string subtitle = string.Format("lvl{0} (+{1} CPS)", upgradeLevel, upgrade.cpsPerLevel);
        string price = upgrade.GetPrice(upgradeLevel).ToString("F0");

        upgradeButton.UpdateVisuals(subtitle,price);
    }

    private double GetUpgradePrice(int upgradeIndex)
    {
        return upgrades[upgradeIndex].GetPrice(GetUpgradeLevel(upgradeIndex));
    }

    public UpgradeSO[] GetUpgrades()
    {
        return upgrades;
    }
}
