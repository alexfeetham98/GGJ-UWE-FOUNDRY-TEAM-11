using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public bool isActive = false;
    public bool isIdle = false;
    public GameObject buildingBase;
    public GameObject buildingHead;
    public float energyCost;
    // Start is called before the first frame update
   public void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void SwitchPower()
    {
        isActive = (isActive = true ? false : true);
    }

    public virtual void Interact()
    {
        SwitchPower();
    }
}
