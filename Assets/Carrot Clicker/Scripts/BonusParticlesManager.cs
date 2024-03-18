using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BonusParticlesManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject bonusParticlePrefab;
    [SerializeField] private CarrotManager carrotManager;

    [Header("Pooling")]
    private ObjectPool<GameObject> bonusParticlePool;
    private void Awake()
    {
        InputManager.onCarrotClickedPosition += CarrotClickedCallback;
    }


    private void OnDestroy()
    {
        InputManager.onCarrotClickedPosition -= CarrotClickedCallback;
    }
    // Start is called before the first frame update
    void Start()
    {
        bonusParticlePool = new ObjectPool<GameObject>(CreateFunction, ActionOnGet, ActionOnRelease, ActionOnDestory);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    private GameObject CreateFunction()
    {
        return Instantiate(bonusParticlePrefab, transform);
    }

    private void ActionOnGet(GameObject bonusParticle)
    {
        bonusParticle.SetActive(true);
    }

    private void ActionOnRelease(GameObject bonusParticle)
    {
        bonusParticle.SetActive(false);
    }

    private void ActionOnDestory(GameObject bonusParticle)
    {
        Destroy(bonusParticle);
    }
    private void CarrotClickedCallback(Vector2 clickedPosition)
    {
        GameObject bonusParticleInstance = bonusParticlePool.Get(); //Havuzdan objemizi çaðýrýyoruz.
        bonusParticleInstance.transform.position = clickedPosition; //Týkladýðýmýz yerde parçacýk çýkmasý için pozisyonunu ayarlýyoruz.
        bonusParticleInstance.GetComponent<BonusParticle>().Configure(carrotManager.GetCurrentMultiplier());

        LeanTween.delayedCall(1, () => bonusParticlePool.Release(bonusParticleInstance));
    }
}
