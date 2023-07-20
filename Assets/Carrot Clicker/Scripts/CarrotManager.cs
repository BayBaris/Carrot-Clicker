using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI carrotCountText;
    [Header("Data")]
    [SerializeField] private double totalCarrotsCount;
    [SerializeField] private double frenzyModeMultiplier;
    private int carrotIncrement;
    private void Awake()
    {
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void UpdateCarrotText()
    {
        carrotCountText.text = totalCarrotsCount + " Carrots!";
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
}
