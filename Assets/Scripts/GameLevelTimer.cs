using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevelTimer : MonoBehaviour
{
    [SerializeField] private float levelDuration = 1200;
    [SerializeField] private int numberOfMoons = 3;
    [SerializeField] private Transform moonHolder;
    [SerializeField] private GameObject moonPrefab;

    public static GameLevelTimer Instance;
    private float levelTimer = 0;
    private bool levelActive = true;

    private List<GameObject> moons = new List<GameObject>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }

        SetupMoons();
        Instance = this;
    }

    private void OnDestroy()
    {
        if(Instance == this)
        {
            Instance = null;
        }
    }

    private void Update()
    {
        if(!levelActive)
        {
            return;
        }

        levelTimer += Time.deltaTime;

        UpdateMoon();

        if (levelTimer > levelDuration)
        {
            StartCoroutine(KillPlayerRoutine());
        }
    }

    private void SetupMoons()
    {
        if(moons.Count > 0)
        {
            foreach(GameObject moon in moons)
            {
                Destroy(moon);
            }

            moons.Clear();
        }

        for(int i = 0; i < numberOfMoons; i++)
        {
            moons.Add(Instantiate(moonPrefab, moonHolder));
        }
    }

    private IEnumerator KillPlayerRoutine()
    {
        yield return new WaitForSeconds(1);

        Player.instance.KillPlayer();
        levelTimer = 0;

        levelActive = false;
    }

    public void UpdateMoon()
    {
        float trashHold = levelDuration / numberOfMoons;

        for(int i = 0;i < numberOfMoons;i++)
        {
            moons[i].SetActive(Mathf.CeilToInt((levelDuration - levelTimer) / trashHold) > i);
        }
    }

    public void ActiveteTimer()
    {
        levelActive = true;
        SetupMoons();
    }
}
