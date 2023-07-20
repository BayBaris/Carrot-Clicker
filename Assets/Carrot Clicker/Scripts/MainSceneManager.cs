using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    private void Awake()
    {
        InputManager.onBackMenuButtonClicked += BackToMenuCallback;
    }


    private void OnDestroy()
    {
        InputManager.onBackMenuButtonClicked -= BackToMenuCallback;
    }

    private void BackToMenuCallback()
    {
        BackToMenu();
        InputManager.onBackMenuButtonClicked?.Invoke();
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
