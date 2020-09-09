using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLineAsset : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    Animator anim;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }
    public void SetOn(bool isOn)
    {
        if (isOn)
            GetComponentInChildren<Animator>().Play("singleLineOn");
        else
            GetComponentInChildren<Animator>().Play("singleLineOff");
    }
}
