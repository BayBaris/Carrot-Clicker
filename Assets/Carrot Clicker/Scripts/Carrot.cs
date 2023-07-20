using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carrot : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform carrotRendererTransform;
    [SerializeField] private Image fillImage;

    [Header("Settings")]
    [SerializeField] private float fillRate;
    private bool isFrenzyModeActive;

    [Header("Actions")]
    public static Action onFrenzyModeStarted;
    public static Action onFrenzyModeStopped;
    private void Awake()
    {
        InputManager.onCarrotClicked += CarrotClickedCallback;
    }

    private void OnDestroy()
    {
        InputManager.onCarrotClicked -= CarrotClickedCallback;
    }

    private void CarrotClickedCallback()
    {
        // Animate Carrot Renderer..
        Animate();
        // Fill Carrot Image..
        if (!isFrenzyModeActive ) 
        {
            Fill();
        }
        
    }

    private void Animate()
    {
        // Vector3.one == 1.0f on transform..
        // Bir kereli�ine loop'a girmesini sa�l�yoruz. B�y�y�p k���lme efektini bu �ekilde sa�l�yoruz. (.setLoopPingPong())
        carrotRendererTransform.localScale = Vector3.one * .8f;
        LeanTween.cancel(carrotRendererTransform.gameObject);
        LeanTween.scale(carrotRendererTransform.gameObject, Vector3.one * 0.7f, .15f).setLoopPingPong(1);
    }

    private void Fill()
    {
        fillImage.fillAmount += fillRate;
        if(fillImage.fillAmount >= 1) 
        {
            StartFrenzyMode();
        }
    }

    private void StartFrenzyMode()
    {
        isFrenzyModeActive = true;
        // 5 saniye boyuncu de�eri 1 => 0 a indir ve bu de�eri fillAmounta ata. ��lem bitince StopFrenzy methodunu �al��t�r.
        LeanTween.value(1, 0, 5).setOnUpdate((value) => fillImage.fillAmount = value)
            .setOnComplete(StopFrenzyMode);
        onFrenzyModeStarted?.Invoke();
    }

    private void StopFrenzyMode()
    {
        isFrenzyModeActive = false;
        onFrenzyModeStopped?.Invoke();
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
