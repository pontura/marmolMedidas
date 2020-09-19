using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnglesSignal : MonoBehaviour
{
    public Text field;
    public Image background;
    VerticeAngle verticeAngle;
    bool isLocked;
    Button button;

    public void Init(VerticeAngle _verticeAngle, bool _isLocked)
    {
        this.isLocked = _isLocked;
        button = GetComponent<Button>();
        if (isLocked)
        {
            SetInteraction(false);
            field.color = Color.white;
            background.color = Color.red;
        }
        else
        {
            field.color = Color.black;
        }
        this.verticeAngle = _verticeAngle;
        field.text = Utils.RoundNumber(verticeAngle.angle, 2).ToString() + "°";        
    }
    public void Clicked()
    {
        MappingManager.Instance.VerticeClicked(verticeAngle);
    }
    public void SetOn()
    {
        GetComponent<Animation>().Play("distanceOn");
        SetInteraction(true);
        field.color = Color.black;
    }
    public void SetOff()
    {
        GetComponent<Animation>().Play("distanceOff");
    }
    public void SetLastAngles()
    {
        gameObject.SetActive(true);
        GetComponent<Animation>().Stop();       
        background.color = Color.black;
        field.color = Color.white;
    }
    void SetInteraction(bool isOn)
    {
        button.interactable = isOn;
    }
}
