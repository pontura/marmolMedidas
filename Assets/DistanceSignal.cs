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
        background.color = Color.black;
        int total = MappingManager.Instance.verticeAngleManager.all.Count;
        Vector3 vertice1 = MappingManager.Instance.verticeAngleManager.all[total-2].transform.position;
        Vector3 vertice2 = MappingManager.Instance.verticeAngleManager.all[total-1].transform.position;
        float distanceInPixels = Vector3.Distance(vertice1, vertice2);
        float value = MappingManager.Instance.verticeAngleManager.GetDistanceInCm(distanceInPixels);
        MappingManager.Instance.verticeAngleManager.data[MappingManager.Instance.verticeAngleManager.data.Count - 1].distance = value;
        field.text = Utils.RoundNumber(value, 2).ToString() + "cm";
    }
}
