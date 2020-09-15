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
            print("nextAngleID " + nextAngleID);
            if (nextAngleID >= MappingManager.Instance.verticeAngleManager.data.Count - 1)
                EndAnglesConfig();
            else if (nextAngleID > 0)
            {
                AnglesSignal anglesSignal = MappingManager.Instance.uiMapping.angles[nextAngleID];
                anglesSignal.gameObject.SetActive(true);
                anglesSignal.SetOn();
            }
        }


    }
    void EndDistanceConfig()
    {
       // MappingManager.Instance.ui.distances[0].gameObject.SetActive(true);
        MappingManager.Instance.ConfirmAngles();
        id = 1;
        SetNextConfirm();
    }
    void EndAnglesConfig()
    {
        MappingManager.Instance.state = MappingManager.states.CONFIRM_ANGLE_LAST_1;

        AnglesSignal anglesSignal = MappingManager.Instance.uiMapping.angles[MappingManager.Instance.uiMapping.angles.Count - 1];
        anglesSignal.gameObject.SetActive(true);
        anglesSignal.SetOn();

        Debug.Log("EndAnglesConfig");
    }
    void SetLastAngle1()
    {
        MappingManager.Instance.uiMapping.distances[0].gameObject.SetActive(true);
        MappingManager.Instance.uiMapping.angles[0].gameObject.SetActive(true);        
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
