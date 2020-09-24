using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostillaPoint : MonoBehaviour
{
    UICostillas ui;
    public void Init(UICostillas ui)
    {
        this.ui = ui;
    }
    public void OnClicked()
    {
        ui.DelettePoint(this);
    }
}
