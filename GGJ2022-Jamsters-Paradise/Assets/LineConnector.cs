using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineConnector : MonoBehaviour
{
    public GameObject[] _points;

    private LineRenderer line;

    private void Start()
    {
        line = this.gameObject.GetComponent<LineRenderer>();
    }

    private void Update()
    {
        for(int i = 0; i < _points.Length; i++)
        {
            line.SetPosition(i, _points[i].transform.position);
        }
    }
}
