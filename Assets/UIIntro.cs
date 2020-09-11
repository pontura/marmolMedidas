using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIIntro : MonoBehaviour
{
    public GameObject panel;
    UIMain uiMain;

    public void Init()
    {
        uiMain = GetComponent<UIMain>();
        panel.SetActive(true);
    }

    public void Clicked(int id)
    {
        uiMain.InitMapping(id);
        panel.SetActive(false);
    }
}
