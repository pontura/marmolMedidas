using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMapping : MonoBehaviour
{
    public List<DistanceSignal> distances;
    public List<AnglesSignal> angles;

    public DistanceSignal distanceSignal;
    public AnglesSignal anglesSignal;
    public Transform container;
    [HideInInspector] public UIAngleInputPanel angleInputPanel;
    [HideInInspector] public UISizeInputPanel sizeInputPanel;
    
    UITools uiTools;
    private void Start()
    {
        uiTools = GetComponent<UITools>();
    }

    public void Init()
    {
        uiTools.Init();
        Events.ReCalculateAll += ReCalculateAll;
        Events.DeleteAll += DeleteAll;

        angleInputPanel = GetComponent<UIAngleInputPanel>();
        sizeInputPanel = GetComponent<UISizeInputPanel>();
    }
   
   
    void OnDestroy()
    {
        Events.ReCalculateAll -= ReCalculateAll;
        Events.DeleteAll -= DeleteAll;
    }
    void ReCalculateAll()
    {
        angles.Clear();
        distances.Clear();
        Utils.RemoveAllChildsIn(container);
    }
    public void OnAddAngle(VerticeAngle verticeAngle, VerticeAngle verticeAngleOther)
    {
        Vector3 pos = MappingManager.Instance.cam.WorldToScreenPoint(verticeAngle.transform.position);
        Vector3 pos2 = MappingManager.Instance.cam.WorldToScreenPoint(verticeAngleOther.transform.position);
        AnglesSignal newAnglesSignal = Instantiate(anglesSignal, pos, Quaternion.identity, container);
        DistanceSignal newDistanceSignal = Instantiate(distanceSignal, Vector3.Lerp(pos, pos2, 0.5f), Quaternion.identity, container);

        int totalVertices = MappingManager.Instance.verticeAngleManager.all.Count-1;
        bool angleLocked = false;
        if (verticeAngle.id == 0 || verticeAngle.id >= totalVertices - 1)
            angleLocked = true;

        bool distanceLocked = false;
        if (verticeAngle.id == 0)
            distanceLocked = true;        

        newAnglesSignal.Init(verticeAngle, angleLocked);
        newDistanceSignal.Init(verticeAngle, distanceLocked);

        angles.Add(newAnglesSignal);
        distances.Add(newDistanceSignal);

        VerticeAngleManager.VerticeData data = MappingManager.Instance.verticeAngleManager.GetVerticeData(verticeAngle.id);

        print("verticeAngle.id: "  + verticeAngle.id + "     angle " + data.angle + "   distance: "+ data.distance);
        if (data.angle == 0)
            newAnglesSignal.gameObject.SetActive(false);
        if (data.distance == 0)
            newDistanceSignal.gameObject.SetActive(false);

    }
    void DeleteAll()
    {
        Utils.RemoveAllChildsIn(container);
        MappingManager.Instance.Reset();
    }
}
