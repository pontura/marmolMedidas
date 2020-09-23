using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMeassure : MonoBehaviour
{
    public GameObject uiMessureButton;
    public GameObject swapButtonOn;
    
    public Transform container;
    public bool isOn;
    public MeassureSignal signal;
    public MessurePoint mPoint;
    public int id;
    VerticeAngle verticeAngle1;
    VerticeAngle verticeAngle2;
    public List<MessurePoint> points;
    public LineRenderer lineRenderer;
    Vector3[] allVertices;

    private void Start()
    {
        Reset();
        swapButtonOn.SetActive(false);
    }
    public void Init()
    {
        uiMessureButton.SetActive(true);
    }
    public void Reset()
    {
        uiMessureButton.SetActive(false);
    }
    public void OnSwapClicked()
    {
        ResetValues();
        id = 0;
        isOn = !isOn;
        if (isOn)
            swapButtonOn.SetActive(true);
        else
        {
            ResetValues();
            swapButtonOn.SetActive(false);
        }
    }
    public void OnAddPoint(VerticeAngle verticeAngle)
    {
        if (id == 0)
        {
            verticeAngle1 = verticeAngle;
            AddPart(verticeAngle1);
        }
        else if (id == 1)
        {
            verticeAngle2 = verticeAngle;
            AddPart(verticeAngle2);
            AddFinalStep();
            AddLine();
        }
        else
        {
            ResetValues();
            return;
        }
        id++;
    }
    public void Close()
    {
        ResetValues();
    }
    public void ResetValues()
    {
        lineRenderer.positionCount = 0;
        verticeAngle1 = null;
        verticeAngle2 = null;
        id = 0;
        Utils.RemoveAllChildsIn(container);
    }
    void AddPart(VerticeAngle verticeAngle)
    {
        MessurePoint newPoint = Instantiate(mPoint, verticeAngle.transform.position, Quaternion.identity, container);
        newPoint.Init(this);
    }
    void AddFinalStep()
    {
        Vector3 pos = Vector3.Lerp(verticeAngle1.transform.position,verticeAngle2.transform.position, 0.5f);
        MeassureSignal newSignal = Instantiate(signal, pos, Quaternion.identity, container);
        newSignal.Init(this, verticeAngle1.transform.position, verticeAngle2.transform.position);
    }
    public void DelettePoint(MessurePoint mPoint)
    {
        ResetValues();
    }
    public void AddLine()
    {
        allVertices = new Vector3[2];
        allVertices[0] = verticeAngle1.transform.position;
        allVertices[1] = verticeAngle2.transform.position;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPositions(allVertices);
    }
}
