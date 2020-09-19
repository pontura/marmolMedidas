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
        field.text = "";
        this.verticeAngle = _verticeAngle;
        if (locked)
        {
            GetComponent<Button>().interactable = false;
            background.color = Color.red;
            field.text = "?";
            field.color = Color.white;
        }
        else
        {
            field.color = Color.black;
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
        {
            field.text = "?";
            SetOn();
        }
    }
    public void Clicked()
    {
        MappingManager.Instance.SizeClicked(verticeAngle);
    }
    public void SetOn()
    {
        GetComponent<Animation>().Play("distanceOn");
    }
    public void SetLastDistance()
    {
        GetComponent<Animation>().Stop();
        GetComponent<Button>().interactable = false;
        gameObject.SetActive(true);
        field.color = Color.white;
        background.color = Color.black;
        float value = MappingManager.Instance.verticeAngleManager.data[MappingManager.Instance.verticeAngleManager.data.Count - 1].distance;
        field.text = Utils.RoundNumber(value, 2).ToString();
    }
}
