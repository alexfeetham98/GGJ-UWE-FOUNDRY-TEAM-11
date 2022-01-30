using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Socket : MonoBehaviour
{
    public GameObject PlugPos;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Plug")
        {
            other.transform.SetPositionAndRotation(PlugPos.transform.position, PlugPos.transform.rotation);
            other.GetComponent<Cable>().frozen = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag =="Plug")
        {
            other.GetComponent<Cable>().frozen = false;
            other.transform.SetPositionAndRotation(
                other.GetComponent<Cable>().plugBase.transform.position, other.GetComponent<Cable>().plugBase.transform.rotation);
        }
    }
}
