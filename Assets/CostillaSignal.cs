using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostillaSignal : MonoBehaviour
{
    public Text field;
    public Image background;
    UICostillas ui;

    public void Init(UICostillas ui, Vector3 pos1, Vector3 pos2)
    {
        float distanceInPixels = Vector3.Distance(pos1, pos2);
        float value = MappingManager.Instance.verticeAngleManager.GetDistanceInCm(distanceInPixels);
        field.text = Utils.RoundNumber(value, 2).ToString() + "cm";
    }
    public void Clicked()
    {
        ui.Reset();
    }
}
