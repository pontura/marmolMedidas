using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VerticeAngleManager : MonoBehaviour
{
    public float normalizedDistance = 1;
    public List<VerticeData> data;
    [Serializable]
    public class VerticeData
    {
        public float angle;
        public float distance;
        public bool distanceChecked;
        public bool angleChecked;
    }
    private void Start()
    {
        Events.DeleteAll += DeleteAll;
        angleDistanceRemapping = GetComponent<AnglesDistanceRemapping>();
    }
    private void OnDestroy()
    {
        Events.DeleteAll -= DeleteAll;
    }

    public void ConfirmDistance(int id)
    {       
        data[id].distanceChecked = true;
    }
    public void ForceSquaredFigure()
    {
        Vector3 pos = all[2].transform.position;
        pos.y = all[3].transform.position.y;
        all[3].transform.position = pos;
    }
    public void ConfirmAngle(int id)
    {
        data[id].angleChecked = true;
    }
    public VerticeData GetVerticeData(int angleID)
    {
        if(angleID== 0)
            return data[data.Count-1];
        else
            return data[angleID - 1];
    }
    public VerticeAngle verticeAngle_to_add;
    public List<VerticeAngle> all;

    public bool[] anglesVerified;
    public bool[] distancesVerified;

    public Transform container;
    public LineAsset lineAsset;
    AnglesDistanceRemapping angleDistanceRemapping;

    public float GetDistanceInCm(float pixels)
    {
        return pixels * normalizedDistance;
    }
  
    public void RefreshVerticeData()
    {
        int id = 0;
        foreach(VerticeAngle va in all)
        {
            float lastDistance_in_pixels = va.distance_in_pixels;
            if (id == 0)
                data[data.Count - 1].distance = GetDistanceInCm(lastDistance_in_pixels);
            else if (id < data.Count)
                data[id - 1].distance = GetDistanceInCm(lastDistance_in_pixels);
            id++;
        }
       
    }
    public void AddVAngle(Vector3 pos, bool isLast = false)
    {
        pos.z = 0;
        VerticeAngle go = Instantiate(verticeAngle_to_add, pos, Quaternion.identity, container);
        all.Add(go);
        lineAsset.Refresh(this);
        go.Init(isLast);        
    }
    public void CloseFigure()
    {
        data.Clear();
        foreach (VerticeAngle v in all)
        {
            v.SetCollidersOff();
            data.Add(new VerticeData());
        }
        AddVAngle(all[0].transform.position, true);
        angleDistanceRemapping.Calculate();
    }
    public void DeleteAll()
    {
        Utils.RemoveAllChildsIn(container);
        all.Clear();
        data.Clear();
    }
    public void ChangeDistance(int angleID, float originalValue, float value)
    {
        float result = 1;
       
        if(angleID - 1 == 0 && data[0].distance == 0)
        {
            //first meassure:
            print("First Time Meassure");
            normalizedDistance = value / originalValue;
        }
        else
        {
            result = value / originalValue / normalizedDistance;
        }
        float distance = originalValue * normalizedDistance;

        data[angleID - 1].distance = value;
        GameObject pivot = new GameObject();
        pivot.transform.position = all[angleID - 1].transform.position;

        VerticeAngle angle2 = all[angleID];
        angle2.transform.SetParent(pivot.transform);

        float d = result;
        pivot.transform.localScale = new Vector3(d,d,d);

        angle2.transform.SetParent(container);
        angle2.transform.localScale = Vector3.one;

        if (!data[angleID - 1].distanceChecked && angleID - 1 == 1 && MappingManager.Instance.forceSquare)
            ForceSquaredFigure();

        Destroy(pivot);
        SetLastVetriceAsFirst();
        angleDistanceRemapping.Calculate();
        lineAsset.Refresh(this);
        RefreshVerticeData();
    }
    public void ChangeAngle(int angleID, float originalValue, float value)
    {
        
        value = originalValue - value;
        GameObject pivot = new GameObject();
        pivot.transform.position = all[angleID].transform.position;
        int id = 0;
        foreach (VerticeAngle verticeAngle in all)
        {
            if (id > angleID)
                verticeAngle.transform.SetParent(pivot.transform);
                id++;
        }
        Vector3 rot = pivot.transform.localEulerAngles;
        rot.z += value;
        pivot.transform.localEulerAngles = rot;

        foreach (VerticeAngle verticeAngle in all)
        {
            if (id > angleID)
                verticeAngle.transform.SetParent(container);
            id++;
        }
        if (angleID == 0)
            data[data.Count].angle = all[angleID].angle;
        else
            data[angleID - 1].angle = all[angleID].angle;

        Destroy(pivot);
        SetLastVetriceAsFirst();
        angleDistanceRemapping.Calculate();
        lineAsset.Refresh(this);

       
    }
    void SetLastVetriceAsFirst()
    {
        all[all.Count - 1].transform.position = all[0].transform.position;
    }
    
}
