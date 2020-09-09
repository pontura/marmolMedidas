using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Confirmations : MonoBehaviour
{
    int id = 1;
    public void Init()
    {
        int totalAngles = MappingManager.Instance.verticeAngleManager.all.Count - 1;
        SetNext();
    }
    public void SetNext()
    { 
        foreach (SingleLineAsset line in MappingManager.Instance.verticeAngleManager.lineAsset.allLines)
            line.SetOn(false);

        if (MappingManager.Instance.state == MappingManager.states.CONFIRM_SIZE)
        {
            MappingManager.Instance.verticeAngleManager.lineAsset.allLines[id - 1].SetOn(true);
            if (MappingManager.Instance.ui.distances.Count > id)
            {
                MappingManager.Instance.ui.distances[id].gameObject.SetActive(true);
                id++;
            }
            else
            {
                MappingManager.Instance.ConfirmAngles();
                id = 0;
            }            
        }
        else if (MappingManager.Instance.state == MappingManager.states.CONFIRM_ANGLES)
        {
            if (MappingManager.Instance.ui.distances.Count > id)
            {
                MappingManager.Instance.ui.distances[id].gameObject.SetActive(true);
                id++;
            }
            else
            {
                MappingManager.Instance.ConfirmAngles();
                id = 0;
            }

        }
    }
}
