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
        CONFIRM_ANGLE_LAST_1,
        CONFIRM_DISTANCE_LAST,
        CONFIRM_ANGLE_LAST_2,
        EDITING
    }

    [HideInInspector] public VerticeAngleManager verticeAngleManager;
    [HideInInspector] public Confirmations confirmations;

    static MappingManager mInstance = null;
    public string newScene;
    public Camera cam;
    public UIMapping uiMapping;
    public UIMain ui;
    public bool forceSquare; // por si eligio partir de un rectangulo:

    public static MappingManager Instance
    {
        get   { return mInstance;  }
    }
    void Awake()
    {
        if (!mInstance)  mInstance = this;
    }
    public void Init(bool forceSquare)
    {
        this.forceSquare = forceSquare;
        gameObject.SetActive(true);
        uiMapping.Init();
        ChangeStateTo(states.SKETCHING);
        verticeAngleManager = GetComponent<VerticeAngleManager>();
        confirmations = GetComponent<Confirmations>();

        if(forceSquare)
            Invoke("ForceSquare", 0.1f);
    }
    public void VerticeClicked(VerticeAngle verticeAngle)
    {
        if (state == states.SKETCHING)
            JumpToConfirm();
        else
            uiMapping.angleInputPanel.Init(verticeAngle);
    }
    public void SizeClicked(VerticeAngle verticeAngle)
    {
        if (state != states.SKETCHING)
            uiMapping.sizeInputPanel.Init(verticeAngle);
    }
    public void JumpToConfirm()
    {
        if (verticeAngleManager.all.Count > 2)
        {
            verticeAngleManager.CloseFigure();
            ChangeStateTo( states.CONFIRM_SIZE);
            confirmations.Init();
        } else
        {
            Reset();
        }
    }
    public void ClickOnFloor(Vector3 pos)
    {
        if (state == states.SKETCHING)
            verticeAngleManager.AddVAngle(pos);
    }
    public void ConfirmAngles()
    {
        ChangeStateTo( states.CONFIRM_ANGLES);
    }
    public void ChangeStateTo(states state)
    {
        this.state = state;
        ui.ChangeState(state);
    }
    public void Reset()
    {
        ChangeStateTo(states.SKETCHING);
        verticeAngleManager.DeleteAll();
    }
    void ForceSquare()
    {
        ClickOnFloor(new Vector3(2, -2, 0));
        ClickOnFloor(new Vector3(2, 2, 0));
        ClickOnFloor(new Vector3(-2, 2, 0));
        ClickOnFloor(new Vector3(-2, -2, 0));
        VerticeClicked(verticeAngleManager.all[0]);
    }
}
