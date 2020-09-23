using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnglesSignal : MonoBehaviour
{
    public Text field;
    public Image background;
    VerticeAngle verticeAngle;
    public bool isLocked;
    Button button;

    public void Init(VerticeAngle _verticeAngle, bool _isLocked)
    {
        this.isLocked = _isLocked;
        button = GetComponent<Button>();

        if (isLocked)
        {
            SetInteraction(false);            
            background.color = Color.red;
        }
        this.verticeAngle = _verticeAngle;
        field.text = Utils.RoundNumber(verticeAngle.angle, 2).ToString() + "°";        
    }
    public void Clicked()
    {
        if (MappingManager.Instance.uImeassure.isOn)
            MappingManager.Instance.uImeassure.OnAddPoint(verticeAngle);
        else
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
        isLocked = true;
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
