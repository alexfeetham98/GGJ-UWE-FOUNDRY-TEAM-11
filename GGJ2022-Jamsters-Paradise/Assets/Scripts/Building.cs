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

    public Material activeMat;
    public Material inactiveMat;

    private MeshRenderer baseRenderer;
    public float buildCost;
    // Start is called before the first frame update
   public void Start()
    {
        if (buildingBase != null)
        {
            baseRenderer = buildingBase.GetComponent<MeshRenderer>();
        }
    }

    // Update is called once per frame
    public void Update()
    {
        
    }

    public void SwitchPower()
    {
        isActive = (isActive == true) ? false : true;
        if (isActive)
        {
            baseRenderer.material = activeMat;
        }
        else
        {
            baseRenderer.material = inactiveMat;
        }
    }

    public virtual void Interact()
    {
        SwitchPower();
    }
}
