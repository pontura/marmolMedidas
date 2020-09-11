using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceSignal : MonoBehaviour
{
    public Text field;
    public Image background;
    public VerticeAngle verticeAngle;

    public void Init(VerticeAngle _verticeAngle, bool locked)
    {
        this.verticeAngle = _verticeAngle;
        if (locked)
        {
            GetComponent<Button>().interactable = false;
            background.color = Color.red;
        }
        else
        {
            Invoke("Delayed", 0.1f);
        }      
    }
    void Delayed()
    {
        if (MappingManager.Instance.verticeAngleManager.data[verticeAngle.id - 1].distanceChecked)
        {
            float distanceInPixels = verticeAngle.distance_in_pixels;
            float distance = MappingManager.Instance.verticeAngleManager.GetDistanceInCm(distanceInPixels);
            field.text = Utils.RoundNumber(distance, 2).ToString() + "cm";
        }
        else
            field.text = "?";
    }
    public void Clicked()
    {
        MappingManager.Instance.SizeClicked(verticeAngle);
    }
}
