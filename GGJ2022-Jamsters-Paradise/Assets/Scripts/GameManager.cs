using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public enum GAMESTATE
    {
        MAIN_MENU,
        GAME_DAY,
        GAME_NIGHT,
        END_SCREEN
    }
    static int BATTERY_ENERGY_VALUE = 50;
    public float CRITICAL_ALERT_DEFAULT_VALUE = 15f;

    public static GameManager gameManager;

    public GAMESTATE gamestate;
    public float currentEnergy;
    public float maxEnergy;
    public float energyFlow;

    public bool criticalAlert = false;
    public float criticalAlertTimer = 15f;
    private List<Battery> batteryList = new List<Battery>();
    public List<Building> buildings = new List<Building>();

    [SerializeField]private Slider energySlider;
    [SerializeField] private TMP_Text alertTimer;
    [SerializeField] private GameObject EndScreenUI;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
        if (gamestate == GAMESTATE.GAME_DAY)
        {
            Init();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gamestate)
        {
            case GAMESTATE.MAIN_MENU:


                break;
            case GAMESTATE.GAME_DAY:

                CalculateEnergy();
                CheckFailstates();

                break;
            case GAMESTATE.GAME_NIGHT:


                break;
            case GAMESTATE.END_SCREEN:


                break;
            default:
                break;
        }


    }



    private void CalculateEnergy()
    {
        energyFlow = 0;
        foreach (var building in buildings)
        {
            if (building.isActive && !building.isIdle)
            {
                energyFlow += building.energyCost;
            }
        }

        currentEnergy += energyFlow * Time.deltaTime;
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);
        energySlider.value = currentEnergy;
    }
    private void CheckFailstates()
    {
        criticalAlert = (currentEnergy <= 0 && energyFlow < 0) ? true : false;

        if (criticalAlert)
        {
            
            alertTimer.text = Mathf.FloorToInt(criticalAlertTimer).ToString();
            if(currentEnergy > 0)
            {
                criticalAlert = false;
                criticalAlertTimer = CRITICAL_ALERT_DEFAULT_VALUE;
            }
            criticalAlertTimer -= Time.deltaTime;


            if (criticalAlertTimer <= 0)
            {
                GameLost();
            }
        }
        else
        {
            if(currentEnergy <= 0 && energyFlow < 0)
            {
                criticalAlert = true;
                criticalAlertTimer = CRITICAL_ALERT_DEFAULT_VALUE;
            }
        }
        if (batteryList.Count == 0)
        {
            GameLost();
        }

    }

    private void GetBatteryCount()
    {
        batteryList.Clear();
        batteryList.AddRange(FindObjectsOfType<Battery>());
        CalculateMaxEnergy();
    }

    private void CalculateMaxEnergy()
    {
        maxEnergy = batteryList.Count * BATTERY_ENERGY_VALUE;
        energySlider.maxValue = maxEnergy;
    }

    private void Init()
    {
        GetBatteryCount();
        currentEnergy = maxEnergy;
        buildings.AddRange(FindObjectsOfType<Building>());
        energySlider.maxValue = maxEnergy;
    }

    private void GameLost()
    {
        Debug.Log("GAME LOST");
    }

    public void LoadMainGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
