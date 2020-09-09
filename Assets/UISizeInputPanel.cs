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

    void Start()
    {
        panel.SetActive(false);
    }
    void Close()
    {
        panel.SetActive(false);
    }
    public void Init(VerticeAngle verticeAngle)//int angleID, float _originalValue)
    {
        this.originalValue = verticeAngle.distance;
        inputField.text = Utils.RoundNumber(verticeAngle.distance, 2).ToString();
        this.angleID = verticeAngle.id;
        panel.SetActive(true);
    }
    public void SetNewValue (float value)
    {
        
        MappingManager.Instance.verticeAngleManager.ChangeDistance(angleID, originalValue, value);
        MappingManager.Instance.confirmations.SetNext();
        Close();
    }
    public void SetValueByField()
    {
        float value;
        if(float.TryParse(inputField.text, out value))
            SetNewValue(value);
    }
}
