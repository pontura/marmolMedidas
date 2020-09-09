using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAsset : MonoBehaviour
{
    LineRenderer lineRenderer;
    VerticeAngleManager verticeAngleManager;
    public Transform lineCollidersContainer;
    public List<SingleLineAsset> allLines;

    public SingleLineAsset singleLineAsset;

    private void Start()
    {
        Events.DeleteAll += DeleteAll;
    }
    private void OnDestroy()
    {
        Events.DeleteAll -= DeleteAll;
    }
    void DeleteAll()
    {
        Utils.RemoveAllChildsIn(transform);
        lineRenderer.positionCount = 0;
    }
    public void Refresh(VerticeAngleManager verticeAngleManager)
    {
        allLines.Clear();
        Utils.RemoveAllChildsIn(lineCollidersContainer);
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        Vector3[] allVertices = new Vector3[verticeAngleManager.all.Count];
        Draw(allVertices, verticeAngleManager.all);
    }
    private void Draw(Vector3[] allVertices, List<VerticeAngle> allAngles)
    {
        if (allAngles.Count > 1)
        {
            foreach (VerticeAngle a in allAngles)
            {
                allVertices[lineRenderer.positionCount] = allAngles[lineRenderer.positionCount].transform.position;
               
                if(lineRenderer.positionCount < allAngles.Count-1)
                    AddColliderToLine(allAngles[lineRenderer.positionCount].transform.position, allAngles[lineRenderer.positionCount + 1].transform.position);

                lineRenderer.positionCount++;
            }
        }
        lineRenderer.SetPositions(allVertices);
    }
    
    private void AddColliderToLine(Vector3 startPoint, Vector3 endPoint)
    {
        SingleLineAsset newsingleLineAsset = Instantiate(singleLineAsset);
        allLines.Add(newsingleLineAsset);
        newsingleLineAsset.transform.parent = lineRenderer.transform;
        float lineWidth = lineRenderer.endWidth;
        float lineLength = Vector3.Distance(startPoint, endPoint);
        newsingleLineAsset.transform.localScale = new Vector3(lineLength, lineWidth*4, 1f);
        newsingleLineAsset.transform.position = Vector3.Lerp(startPoint, endPoint, 0.5f);
        float angle = Mathf.Atan2((endPoint.y - startPoint.y), (endPoint.x - startPoint.x));
        angle *= Mathf.Rad2Deg;
        newsingleLineAsset.transform.Rotate(0, 0, angle);
    }
}
