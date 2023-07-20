using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private CarrotManager carrotManager;
    private void Awake()
    {
        InputManager.onPlayButtonClicked += PlayButtonCallback;
    }

    private void OnDestroy()
    {
        InputManager.onPlayButtonClicked -= PlayButtonCallback;
    }

    private void PlayButtonCallback()
    {
        StartGame();
        InputManager.onPlayButtonClicked?.Invoke();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ResetCarrot()
    {
        carrotManager.ResetData();
    }
}
