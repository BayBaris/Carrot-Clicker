using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusParticlesManager : MonoBehaviour
{
    [Header("Elemnts")]
    [SerializeField] private GameObject bonusParticlePrefab;
    [SerializeField] private CarrotManager carrotManager;
    private void Awake()
    {
        InputManager.onCarrotClickedPosition += CarrotClickedCallback;
    }


    private void OnDestroy()
    {
        InputManager.onCarrotClickedPosition -= CarrotClickedCallback;
    }
    private void CarrotClickedCallback(Vector2 clickedPosition)
    {
        GameObject bonusParticleInstance = Instantiate(bonusParticlePrefab,clickedPosition,Quaternion.identity, transform);
        bonusParticleInstance.GetComponent<BonusParticle>().Configure(carrotManager.GetCurrentMultiplier());
        Destroy(bonusParticleInstance, 1);
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
