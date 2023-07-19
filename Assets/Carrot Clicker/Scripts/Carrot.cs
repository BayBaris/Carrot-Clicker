using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform carrotRendererTransform;
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
        // Vector3.one == 1.0f on transform..
        // Bir kereliðine loop'a girmesini saðlýyoruz. Büyüyüp küçülme efektini bu þekilde saðlýyoruz. (.setLoopPingPong())
        carrotRendererTransform.localScale = Vector3.one * .8f;
        LeanTween.cancel(carrotRendererTransform.gameObject);
        LeanTween.scale(carrotRendererTransform.gameObject, Vector3.one * 0.7f, .15f).setLoopPingPong(1);
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
