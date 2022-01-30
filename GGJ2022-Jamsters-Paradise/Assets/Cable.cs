using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public List<GameObject> points;
    public List<GameObject> basePoints;
    public GameObject plugBase;

    private Vector3 mOffset;
    private float mZCoord;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        ResetCable();
        ResetCable();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;

        mousePoint.z = mZCoord;

        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mOffset;
    }

    private void ResetCable()
    {
        for (int i = 0; i < points.Count; i++)
        {
            points[i].transform.position = basePoints[i].transform.position;
        }
        this.gameObject.transform.position = plugBase.transform.position;
    }
}