using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISizeInputPanel : MonoBehaviour
{
    public GameObject panel;
    int angleID;
    public InputField inputField;
    float originalValue;
    VerticeAngle verticeAngle;

    void Start()
    {
        panel.SetActive(false);
       
    }
    public void Close()
    {
        panel.SetActive(false);
    }
    public void Init(VerticeAngle verticeAngle)//int angleID, float _originalValue)
    {
        VerticeAngleManager verticeAngleManager = MappingManager.Instance.verticeAngleManager;

        this.verticeAngle = verticeAngle;
        float distance = 0;

        if (verticeAngleManager.data[verticeAngle.id- 1].distanceChecked)
            distance = verticeAngleManager.GetDistanceInCm(verticeAngle.distance_in_pixels);

       
        this.originalValue = verticeAngle.distance_in_pixels;       
        inputField.text = Utils.RoundNumber(distance, 2).ToString();
        this.angleID = verticeAngle.id;
        panel.SetActive(true);
    }
    public void Help()
    {
        Events.OnTutorial(UITutorial.types.MEDIDAS, null);
    }
    public void SetNewValue (float value)
    {
        VerticeAngleManager verticeAngleManager = MappingManager.Instance.verticeAngleManager;
        verticeAngleManager.ChangeDistance(angleID, originalValue, value);
        verticeAngleManager.ConfirmDistance(verticeAngle.id - 1);
        MappingManager.Instance.confirmations.SetNextConfirm();
        Close();
    }
    public void SetValueByField()
    {
        float value;
        if(float.TryParse(inputField.text, out value))
            SetNewValue(value);
    }
}
