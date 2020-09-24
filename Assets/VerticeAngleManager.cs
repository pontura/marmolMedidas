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
        public bool angleLocked;
        public bool is180Angle;
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
    public void AddVAngleCostilla(VerticeAngle originalAngle, Vector3 pos)
    {
        pos.z = 0;
        VerticeAngle go = Instantiate(verticeAngle_to_add, pos, Quaternion.identity, container);
        all.Insert(originalAngle.id, go);
        lineAsset.Refresh(this);
        go.Init(false);
        VerticeData vd = new VerticeData();
        vd.angle = 179.99f;
        int id = originalAngle.id - 1;
        if (id < 0)  id = data.Count - 1;
        vd.distance = data[id].distance / 2;
        vd.angleChecked = true;
        vd.angleLocked = true;
        vd.distanceChecked = true;
        data.Insert(id, vd);
        angleDistanceRemapping.Calculate();
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
        MappingManager.Instance.ChangeStateTo(MappingManager.states.CONFIRM_SIZE);
        float scaleChanged = 1;
       
        if(angleID - 1 == 0 && data[0].distance == 0)
        {
            //first meassure:
           // print("First Time Meassure");
            normalizedDistance = value / originalValue;
        }
        else
        {
            scaleChanged = value / originalValue / normalizedDistance;
        }
        float distance = originalValue * normalizedDistance;

        data[angleID - 1].distance = value;

        RePositionateVertices(angleID, scaleChanged);

        //  if (!data[angleID - 1].distanceChecked && angleID - 1 == 1 && MappingManager.Instance.forceSquare)
        //      ForceSquaredFigure();

        
        SetLastVetriceAsFirst();
        angleDistanceRemapping.Calculate();
        lineAsset.Refresh(this);
        RefreshVerticeData();
        Invoke("Recenter", 0.1f);
    }
    void RePositionateVertices(int angleID, float scaleChanged)
    {
        VerticeAngle a1 = all[angleID]; //el angulo1 a mover
        Vector3 originalPos = a1.transform.position;

        GameObject pivot = new GameObject();
        pivot.transform.position = all[angleID - 1].transform.position;
        a1.transform.SetParent(pivot.transform);
        pivot.transform.localScale = new Vector3(scaleChanged, scaleChanged, scaleChanged);
        a1.transform.SetParent(container);
        Destroy(pivot);

        a1.transform.localScale = Vector3.one;

        Vector3 diffPosition = originalPos - a1.transform.position;

        for (int a = angleID+1; a < all.Count - 1; a++)
        {
            all[a].transform.position -= diffPosition;
        }
        

    }
    public void ChangeAngle(int angleID, float originalValue, float value)
    {
       
        all[angleID].angle = value;
        float pivotRotation;

        bool isOpeningAnlge = IsCenterNearer(angleID);
        if (isOpeningAnlge)
            pivotRotation = originalValue - value;
        else
            pivotRotation = value - originalValue;

       // print("change angle " + angleID + "    originalValue : " + originalValue + "     value: " + value + " pivotRotation_ " + pivotRotation + " isOpeningAnlge: " + isOpeningAnlge);

        GameObject pivot = new GameObject();
        pivot.transform.position = all[angleID].transform.position;

        foreach (VerticeAngle verticeAngle in all)
        {
            if (verticeAngle.id > angleID)
                verticeAngle.transform.SetParent(pivot.transform);
        }
        Vector3 rot = pivot.transform.localEulerAngles;
        rot.z = pivotRotation;
        pivot.transform.localEulerAngles = rot;

        foreach (VerticeAngle verticeAngle in all)
        {
            if (verticeAngle.id > angleID)
                verticeAngle.transform.SetParent(container);
        }
        VerticeData vData;
        if (angleID == 0)
            vData = data[data.Count];
        else
            vData = data[angleID - 1];

        vData.angle = all[angleID].angle;

        if (all[angleID].angle > 179.98f)
            vData.is180Angle = true;

        Destroy(pivot);
        SetLastVetriceAsFirst();
        angleDistanceRemapping.Calculate();
        lineAsset.Refresh(this);
        Invoke("Recenter", 0.1f);
       
    }
    bool IsCenterNearer(int angleID)// se fija que el angulo se abra hacia el centro o se cierre:
    {
        Vector3 myPos = all[angleID].transform.position;
        Vector3 anglePrevPos = all[angleID - 1].transform.position;
        Vector3 angleNextPos = all[angleID + 1].transform.position;
        Vector3 centerPos = Vector3.Lerp(anglePrevPos, angleNextPos, 0.5f);
        if ((Mathf.Abs(centerPos.x) + Mathf.Abs(centerPos.y)) < (Mathf.Abs(myPos.x) + Mathf.Abs(myPos.y)))
            return true;
        return false;
    }
    void SetLastVetriceAsFirst()
    {
        all[all.Count - 1].transform.position = all[0].transform.position;
    }
    void Recenter()
    {
        Events.Recenter();
    }
    
}
