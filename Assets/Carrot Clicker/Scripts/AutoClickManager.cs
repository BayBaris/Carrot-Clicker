using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AutoClickManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Transform rotator;
    [SerializeField] private GameObject bunnyPrefab;

    [Header("Settings")]
    [SerializeField] private float rotatorSpeed;
    [SerializeField] private float rotatorRadius;

    [Header("Data")]
    [SerializeField] private int level;
    [SerializeField] private float carrotsPerSecond;
    [SerializeField] private int currentBunnyIndex;

    private List<GameObject> bunnyPool = new List<GameObject>();
    private List<bool> rotatorActiveChild = new List<bool>();

    private void Awake()
    {
        CreateBunnyPool();
        ShopManager.onUpgradePurchased += CheckIfCanUpgrade;
    }

    private void OnDestroy()
    {
        ShopManager.onUpgradePurchased -= CheckIfCanUpgrade;
    }


    // Start is called before the first frame update
    void Start()
    {
        LoadData();
        carrotsPerSecond = level * .1f;

        InvokeRepeating("AddCarrots", 1, 1);
        SpawnBunnies();
        StartAnimatingBunnies();
    }

    // Update is called once per frame
    void Update()
    {
        rotator.Rotate(Vector3.forward * Time.deltaTime * rotatorSpeed);
    }
    private void CheckIfCanUpgrade(int upgradeIndex)
    {
        if(upgradeIndex == 0)
            Upgrade();
    }


    //Her seferinde tekrar tekrar Instantiate etmek yerine tek seferde hepsini sahnemize ekliyoruz.
    private void CreateBunnyPool()
    {
        for (int i = 0; i < 18; i++)
        {
            GameObject bunnyInstance = Instantiate(bunnyPrefab, Vector3.zero, Quaternion.identity, rotator);
            bunnyInstance.SetActive(false);
            bunnyPool.Add(bunnyInstance);
            
            float angle = i * 20; // 360 derecelik çemberdeki tavþanlarýn arasýndaki mesafeyi belirleyecek olan açý (360/18 = 20) 

            Vector2 position = new Vector2();
            position.x = rotatorRadius * Mathf.Cos(angle * Mathf.Deg2Rad);
            position.y = rotatorRadius * Mathf.Sin(angle * Mathf.Deg2Rad);

            bunnyPool[i].transform.position = position;
            bunnyPool[i].transform.up = position.normalized;
        }
    }

    private void SpawnBunnies()
    {
        for (int i = 0; i < level; i++)
        {
            if (!bunnyPool[i].activeInHierarchy)
            {
                bunnyPool[i].SetActive(true);
                rotatorActiveChild.Add(bunnyPool[i].activeInHierarchy);
            }
        }
    }

    private void AddCarrots()
    {
        CarrotManager.instance.AddCarrots(carrotsPerSecond);
    }

    public void Upgrade()
    {
        level++;
        carrotsPerSecond = level * .1f;

        if (level <= 18)
        {
            SpawnBunnies();
            StartAnimatingBunnies();
        }
    }

    private void StartAnimatingBunnies()
    {
        if (rotatorActiveChild == null) return;
        LeanTween.cancel(gameObject);

        for (int i = 0; i < rotatorActiveChild.Count; i++)
        {
            LeanTween.cancel(rotator.GetChild(i).gameObject);
        }

        LeanTween.moveLocalY(rotator.GetChild(currentBunnyIndex).GetChild(0).gameObject, -0.5f, .25f)
            .setLoopPingPong(1)
            .setOnComplete(AnimateNextBunny);
    }

    private void AnimateNextBunny()
    {
        currentBunnyIndex++;
        if (currentBunnyIndex >= rotatorActiveChild.Count)
        {
            ResetBunniesAnimation();
        }
        else
            StartAnimatingBunnies();
    }

    private void ResetBunniesAnimation()
    {
        currentBunnyIndex = 0;

        float delay = MathF.Max(20 - rotatorActiveChild.Count, 0);

        LeanTween.delayedCall(gameObject, delay, StartAnimatingBunnies);
    }

    private void LoadData()
    {
        level = ShopManager.instance.GetUpgradeLevel(0);
    }
}
