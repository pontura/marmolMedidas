using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnglesSignal : MonoBehaviour
{
    public GameObject angle180;
    public Text field;
    public Image background;
    VerticeAngle verticeAngle;
    VerticeAngleManager.VerticeData verticeData;
    Button button;

    public void Init(VerticeAngle _verticeAngle, VerticeAngleManager.VerticeData verticeData)
    {
        this.verticeData = verticeData;
        button = GetComponent<Button>();
       
        if (_verticeAngle.id == 0 || _verticeAngle.id >= MappingManager.Instance.verticeAngleManager.all.Count - 1)
        {
            SetInteraction(false);            
            background.color = Color.red;
        }
        this.verticeAngle = _verticeAngle;
        field.text = Utils.RoundNumber(verticeAngle.angle, 1).ToString() + "°";

        if (verticeData.is180Angle)
        {
            angle180.SetActive(true);
            field.text = "";
        } else
            angle180.SetActive(false);
    }
    public void Clicked()
    {
        if (MappingManager.Instance.uICostillas.isOn)
            MappingManager.Instance.uICostillas.OnAddPoint(verticeAngle);
        else if(MappingManager.Instance.uImeassure.isOn)
            MappingManager.Instance.uImeassure.OnAddPoint(verticeAngle);
        else if (
            //!verticeData.is180Angle && 
            !verticeData.angleLocked)
            MappingManager.Instance.VerticeClicked(verticeAngle);
    }
    public void SetOn()
    {
        GetComponent<Animation>().Play("distanceOn");
        SetInteraction(true);
    }
    public void SetOff()
    {
        GetComponent<Animation>().Play("distanceOff");
    }
    public void SetLastAngles()
    {
        verticeData.angleLocked = true;
        gameObject.SetActive(true);
        GetComponent<Animation>().Stop();       
        background.color = Color.black;
        SetInteraction(true);
    }
    void SetInteraction(bool isOn)
    {
        button.interactable = isOn;
    }
}
