using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAngleInputPanel : MonoBehaviour
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
    void Close()
    {
        panel.SetActive(false);
    }
    public void Init(VerticeAngle verticeAngle)//int angleID, float _originalValue)
    {
        this.verticeAngle = verticeAngle;
        this.originalValue = verticeAngle.angle;
        inputField.text = Utils.RoundNumber(verticeAngle.angle, 2).ToString();
        this.angleID = verticeAngle.id;
        panel.SetActive(true);
    }
    public void SetNewAngle(float value)
    {

        MappingManager.Instance.verticeAngleManager.ChangeAngle(angleID, originalValue, value);
        MappingManager.Instance.verticeAngleManager.ConfirmAngle(verticeAngle.id - 1);

        MappingManager.Instance.confirmations.SetNextConfirm();
        Close();
    }
    public void SetValueByField()
    {
        float value;
        if(float.TryParse(inputField.text, out value))
            SetNewAngle(value);
    }
}
