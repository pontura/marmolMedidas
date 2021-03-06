﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapping : MonoBehaviour
{
    public List<DistanceSignal> distances;
    public List<AnglesSignal> angles;

    public DistanceSignal distanceSignal;
    public AnglesSignal anglesSignal;
    public Transform container;
    public UIAngleInputPanel angleInputPanel;
    public UISizeInputPanel sizeInputPanel;
    public UITools uiTools;


    public void Init()
    {
        uiTools.Init();
        Events.ReCalculateAll += ReCalculateAll;
        Events.DeleteAll += DeleteAll;

    }
    public void RepositionateByCam(Vector3 repos)
    {
        //print(repos);
        //foreach (DistanceSignal go in distances)
        //    go.transform.position += repos;

        //foreach (AnglesSignal go in angles)
        //    go.transform.position += repos;
    }
   
    void OnDestroy()
    {
        Events.ReCalculateAll -= ReCalculateAll;
        Events.DeleteAll -= DeleteAll;
    }
    void ReCalculateAll()
    {
        angles.Clear();
        distances.Clear();
        Utils.RemoveAllChildsIn(container);
    }
    public void OnAddAngle(VerticeAngle verticeAngle, VerticeAngle verticeAngleOther)
    {     
        Vector3 pos = verticeAngle.transform.position;
        Vector3 pos2 = verticeAngleOther.transform.position;
        pos.z = pos2.z = 0;
        AnglesSignal newAnglesSignal = Instantiate(anglesSignal, pos, Quaternion.identity, container);
        DistanceSignal newDistanceSignal = Instantiate(distanceSignal, Vector3.Lerp(pos, pos2, 0.5f), Quaternion.identity, container);

        int totalVertices = MappingManager.Instance.verticeAngleManager.all.Count-1;

        bool angleLocked = false;
        if (verticeAngle.id == 0 || verticeAngle.id >= totalVertices - 1)
            angleLocked = true;

       

        VerticeAngleManager.VerticeData data = MappingManager.Instance.verticeAngleManager.GetVerticeData(verticeAngle.id);

        newAnglesSignal.Init(verticeAngle, data);
        newDistanceSignal.Init(verticeAngle, data);

        angles.Add(newAnglesSignal);
        distances.Add(newDistanceSignal);

       

        if (data.angle == 0)
            newAnglesSignal.gameObject.SetActive(false);
        if (data.distance == 0)
            newDistanceSignal.gameObject.SetActive(false);

    }
    void DeleteAll()
    {
        Utils.RemoveAllChildsIn(container);
        MappingManager.Instance.Reset();
    }
}
