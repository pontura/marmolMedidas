using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MappingManager : MonoBehaviour
{
    public states state;
    public enum states
    {
        SKETCHING,
        CONFIRM_SIZE,
        CONFIRM_ANGLES,
        EDITING
    }

    [HideInInspector] public VerticeAngleManager verticeAngleManager;
    [HideInInspector] public Confirmations confirmations;

    static MappingManager mInstance = null;
    public string newScene;
    public Camera cam;
    public UIMapping ui;



    public static MappingManager Instance
    {
        get   { return mInstance;  }
    }
    void Awake()
    {
        if (!mInstance)  mInstance = this;
    }
    void Start()
    {
        state = states.SKETCHING;
        verticeAngleManager = GetComponent<VerticeAngleManager>();
        confirmations = GetComponent<Confirmations>();
    }
    public void VerticeClicked(VerticeAngle verticeAngle)
    {
        if (state == states.SKETCHING)
            JumpToConfirm();
        else
            ui.angleInputPanel.Init(verticeAngle);
    }
    public void SizeClicked(VerticeAngle verticeAngle)
    {
        if (state != states.SKETCHING)
            ui.sizeInputPanel.Init(verticeAngle);
    }
    public void JumpToConfirm()
    {
        verticeAngleManager.CloseFigure();
        state = states.CONFIRM_SIZE;
        confirmations.Init();
    }
    public void ClickOnFloor(Vector3 pos)
    {
        if (state == states.SKETCHING)
            verticeAngleManager.AddVAngle(pos);
    }
    public void ConfirmAngles()
    {
        state = states.CONFIRM_ANGLES;
    }
}
