using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessurePoint : MonoBehaviour
{
    UIMeassure uiMessure;
    public void Init(UIMeassure uiMessure)
    {
        this.uiMessure = uiMessure;
    }
    public void OnClicked()
    {
        uiMessure.DelettePoint(this);
    }
}
