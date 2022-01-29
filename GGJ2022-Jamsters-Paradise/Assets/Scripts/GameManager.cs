using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager;

    public float currentEnergy;
    public float maxEnergy;
    public float energyDrain;
    public float energyGain;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = this;
    }

    // Update is called once per frame
    void Update()
    {


        currentEnergy += energyGain - energyDrain;
        Mathf.Clamp(currentEnergy, 0, maxEnergy);
    }
}
