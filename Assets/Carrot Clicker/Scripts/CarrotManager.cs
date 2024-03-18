using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    public static CarrotManager instance;

    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI carrotCountText;
    [Header("Data")]
    [SerializeField] private double totalCarrotsCount;
    [SerializeField] private double frenzyModeMultiplier;
    private int carrotIncrement;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        
        LoadData();
        carrotIncrement = 1;

        //Subscribe action on Awake;
        InputManager.onCarrotClicked += CarrotClickedCallback;
        Carrot.onFrenzyModeStarted += FrenzyModeStartedCallback;
        Carrot.onFrenzyModeStopped += FrenzyModeStoppedCallback;
    }

    private void OnDestroy()
    {
        //Unsubscribe action on OnDestory
        InputManager.onCarrotClicked -= CarrotClickedCallback;
        Carrot.onFrenzyModeStarted -= FrenzyModeStartedCallback;
        Carrot.onFrenzyModeStopped -= FrenzyModeStoppedCallback;
    }

    private void CarrotClickedCallback()
    {
        totalCarrotsCount += carrotIncrement;
        UpdateCarrotText();
        SaveData();
    }

    private void FrenzyModeStartedCallback()
    {
        carrotIncrement = (int)frenzyModeMultiplier;
    }

    private void FrenzyModeStoppedCallback()
    {
        carrotIncrement = 1;
    }

    void Start()
    {
        AddCarrots(5000);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddCarrots(double value)
    {
        totalCarrotsCount += value;

        UpdateCarrotText();
        SaveData();
    } 
    public void AddCarrots(float value)
    {
        totalCarrotsCount += value;

        UpdateCarrotText();
        SaveData();
    }

    public bool TryPurchase(double price)
    {
        if (price <= totalCarrotsCount)
        {
            totalCarrotsCount -= price;
            return true;
        }

        return false;
    }

    private void UpdateCarrotText()
    {
        carrotCountText.text = totalCarrotsCount.ToString("F0") + " Carrots!";
    }

    private void SaveData()
    {
        PlayerPrefs.SetString("Carrots", totalCarrotsCount.ToString());
    }

    private void LoadData()
    {
        double.TryParse(PlayerPrefs.GetString("Carrots"), out totalCarrotsCount);
        UpdateCarrotText();
    }

    public void ResetData()
    {
        PlayerPrefs.DeleteAll();
    }

    public int GetCurrentMultiplier()
    {
        return carrotIncrement;
    }
}
