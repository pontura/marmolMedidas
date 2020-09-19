using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirmations : MonoBehaviour
{
    int id;
    public void Init()
    {
        int totalAngles = MappingManager.Instance.verticeAngleManager.all.Count - 1;
        id = 1;
        SetNextConfirm();
    }
    public void SetNextConfirm()
    { 
        foreach (SingleLineAsset line in MappingManager.Instance.verticeAngleManager.lineAsset.allLines)
            line.SetOn(false);

        if (MappingManager.Instance.state == MappingManager.states.CONFIRM_SIZE)
        {
            int nextDistanceID = GetNextDistance();

            if (nextDistanceID >= MappingManager.Instance.verticeAngleManager.data.Count)
                EndDistanceConfig();
            else if (nextDistanceID > 0)
            {
                MappingManager.Instance.verticeAngleManager.lineAsset.allLines[nextDistanceID - 1].SetOn(true);
                MappingManager.Instance.uiMapping.distances[nextDistanceID].gameObject.SetActive(true);
            }
        }
        else if (MappingManager.Instance.state == MappingManager.states.CONFIRM_ANGLES)
        {
            int nextAngleID = GetNextAngle();
            if (nextAngleID >= MappingManager.Instance.verticeAngleManager.data.Count - 1)
            {
                EndAngleConfig();
            }
            else if (nextAngleID > 0)
                SetAngleOn(nextAngleID, true);
        }
        if (MappingManager.Instance.state == MappingManager.states.CONFIRM_LAST_DISTANCE)
        {
            EndAngleConfig();
        }
    }
    void EndDistanceConfig()
    {
        MappingManager.Instance.ConfirmAngles();
        id = 1;
        SetNextConfirm();
    }
    void EndAngleConfig()
    {
        print("EndAngleConfig");
        MappingManager.Instance.ChangeStateTo(MappingManager.states.CONFIRM_LAST_DISTANCE);
        SetAngleOn(0, false);
        SetAngleOn(MappingManager.Instance.uiMapping.angles.Count - 1, false);
        DistanceSignal ds = MappingManager.Instance.uiMapping.distances[0];
        ds.SetLastDistance();
    }
    void SetAngleOn(int angleID, bool canBeClicked)
    {
        AnglesSignal anglesSignal = MappingManager.Instance.uiMapping.angles[angleID];
        anglesSignal.gameObject.SetActive(true);
        if (canBeClicked)
            anglesSignal.SetOn();
        else
            anglesSignal.SetLastAngles();
    }
    int GetNextDistance()
    {
        int id = 1;
        foreach (VerticeAngleManager.VerticeData data in MappingManager.Instance.verticeAngleManager.data)
        {
            if (!data.distanceChecked)
                return id;
            id++;
        }
        return 0;
    }
    int GetNextAngle()
    {
        int id = 1;
        foreach (VerticeAngleManager.VerticeData data in MappingManager.Instance.verticeAngleManager.data)
        {
            if (!data.angleChecked)
                return id;
            id++;
        }
        return 0;
    }
}
