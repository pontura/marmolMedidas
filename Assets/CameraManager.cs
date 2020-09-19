﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public UIMapping uiMapping;
    public Vector3 dest;
    void Start()
    {
        dest = transform.position;
        Events.Recenter += Recenter;
    }
    void OnDestroy()
    {
        Events.Recenter += Recenter;
    }
    void Recenter()
    {
        Vector3 centerPosition = Vector3.zero;
        int total = 0;
        foreach (VerticeAngle go in MappingManager.Instance.verticeAngleManager.container.GetComponentsInChildren<VerticeAngle>()) {
            if(total>0)
                centerPosition += go.transform.position;
            total++;
        }
        centerPosition /= total-1;
        dest = centerPosition;
        dest.z = -10;
        dest.y -= 1;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, dest, 0.1f);
    }
}
