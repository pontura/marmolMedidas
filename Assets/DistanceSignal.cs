using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSignal : MonoBehaviour
{
    public Text field;
    public Image background;
    VerticeAngle verticeAngle;

    public void Init(VerticeAngle _verticeAngle, bool locked)
    {
        if (locked)
        {
            GetComponent<Button>().interactable = false;
            background.color = Color.red;
        }
        this.verticeAngle = _verticeAngle;
        field.text = Utils.RoundNumber(verticeAngle.distance, 2).ToString() + "cm";
    }
    public void Clicked()
    {
        MappingManager.Instance.SizeClicked(verticeAngle);
    }
}
