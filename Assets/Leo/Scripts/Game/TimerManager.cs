using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public class TimerManager : MonoBehaviour
{
    public static TimerManager Instance;

    [SerializeField]
    private int currentLevel;
    private float currentTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (gameState == GameState.story)
            Destroy(gameObject); 
        IncreaseTime();
        //print(currentTime);
    }

    public void SetFinalTime()
    {
        switch (currentLevel)
        {
            case 1: 
                if (PlayerPrefs.GetInt("level1Time") == 0 || GetFinalTime() < PlayerPrefs.GetInt("level1Time"))
                    GameManager.Instance.SetLevel1Time(GetFinalTime()); 
                break;
            case 2:
                if (PlayerPrefs.GetInt("level2Time") == 0 || GetFinalTime() < PlayerPrefs.GetInt("level2Time"))
                    GameManager.Instance.SetLevel2Time(GetFinalTime());
                break;
            case 3:
                if (PlayerPrefs.GetInt("level3Time") == 0 || GetFinalTime() < PlayerPrefs.GetInt("level3Time"))
                    GameManager.Instance.SetLevel3Time(GetFinalTime());
                break;
            case 4:
                if (PlayerPrefs.GetInt("level4Time") == 0 || GetFinalTime() < PlayerPrefs.GetInt("level4Time"))
                    GameManager.Instance.SetLevel4Time(GetFinalTime());
                break;
            default: 
                break;
        }
        Destroy(gameObject);
    }

    private void IncreaseTime() => currentTime += Time.deltaTime;

    private int GetFinalTime()
    {
        return Convert.ToInt32(currentTime);
    }
}
