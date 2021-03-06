﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMain : MonoBehaviour
{
    public MappingManager mapping;
    public Text title;
    UIIntro uiIntro;

    void Start()
    {
        uiIntro = GetComponent<UIIntro>();

        uiIntro.Init();
        mapping.gameObject.SetActive(false);
    }
    void ResetUIs()
    {
        MappingManager.Instance.uImeassure.SetOff();
        MappingManager.Instance.uICostillas.SetOff();
    }
    public void ChangeState(MappingManager.states state)
    {
        switch (state)
        {
            case MappingManager.states.SKETCHING: SetTitle("Marca los vertices externos"); ResetUIs(); break;
            case MappingManager.states.CONFIRM_SIZE: SetTitle("Mide cada lado"); break;
            case MappingManager.states.CONFIRM_ANGLES: SetTitle("Confirma cada ángulo"); break;
            case MappingManager.states.EDITING: SetTitle("Edición final"); break;
            case MappingManager.states.CONFIRM_LAST_DISTANCE: SetTitle("Confirma la última medida (en negro)"); break;
        }
    }
    void SetTitle(string text)
    {
        title.text = text;
    }
    public void InitMapping(int id)
    {
        switch (id)
        {
            case 0:
                mapping.Init(true);
                break;
            case 1:
                mapping.Init(false);
                break;
        } 
        Events.OnTutorial(UITutorial.types.MEDIDAS, null);
    }
}
