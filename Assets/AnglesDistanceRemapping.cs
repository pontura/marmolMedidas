using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnglesDistanceRemapping : MonoBehaviour
{
    public float angle;
    VerticeAngleManager verticesAnglesManager;
    
    private void Start()
    {
        verticesAnglesManager = GetComponent<VerticeAngleManager>();
    }
    public void Calculate()
    {
        if (verticesAnglesManager.all.Count < 3) return;

        Events.ReCalculateAll();
        int id = 0;
        for (int a = 0; a < verticesAnglesManager.all.Count; a++)
        {
            int first = a -1;
            int mid = a ;
            int last = a + 1;

            if (a == 0)
                first = verticesAnglesManager.all.Count - 2;
            else if (a == verticesAnglesManager.all.Count - 2)
                last = 0;
            else if (a == verticesAnglesManager.all.Count - 1)
                return;


            Vector3 firstAngle = verticesAnglesManager.all[first].transform.position;
            Vector3 midAngle = verticesAnglesManager.all[mid].transform.position;
            Vector3 lastAngle = verticesAnglesManager.all[last].transform.position;

            float angle = Utils.GetVectorInternalAngle(midAngle, lastAngle, firstAngle);

            verticesAnglesManager.all[mid].id = id;
            verticesAnglesManager.all[mid].angle = angle;
            verticesAnglesManager.all[mid].distance_in_pixels = Vector3.Distance(firstAngle, midAngle);

            MappingManager.Instance.uiMapping.OnAddAngle(verticesAnglesManager.all[mid], verticesAnglesManager.all[first]);
            id++;
        }

    }
}
