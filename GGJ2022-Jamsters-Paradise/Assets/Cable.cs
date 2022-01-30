using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cable : MonoBehaviour
{
    public List<GameObject> points;
    public List<GameObject> basePoints;
    public GameObject plugBase;
    public bool frozen = false;

    private Vector3 mOffset;
    private float mZCoord;

    private void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private void OnMouseUp()
    {
        if (frozen)
        {
            return;
        }

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
        //transform.position = new Vector3(Input.mousePosition.x, 0.4f, Input.mousePosition.y);
        transform.position = GetMouseWorldPos() + mOffset;
        
    }

    public void ResetCable()
    {
        for (int i = 0; i < points.Count; i++)
        {
            points[i].transform.position = basePoints[i].transform.position;
        }
        this.gameObject.transform.position = plugBase.transform.position;
    }
}