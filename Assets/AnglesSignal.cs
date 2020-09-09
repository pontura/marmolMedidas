﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnglesSignal : MonoBehaviour
{
    public Text field;
    public Image background;
    VerticeAngle verticeAngle;
    bool isLocked;

    public void Init(VerticeAngle _verticeAngle, bool _isLocked)
    {
        this.isLocked = _isLocked;
        if (isLocked)
        {
            GetComponent<Button>().interactable = false;
            background.color = Color.red;
        }
        this.verticeAngle = _verticeAngle;
        field.text = Utils.RoundNumber(verticeAngle.angle, 2).ToString() + "°";        
    }
    public void Clicked()
    {
        MappingManager.Instance.VerticeClicked(verticeAngle);
    }
}
