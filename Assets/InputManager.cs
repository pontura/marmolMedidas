using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MappingManager mappingManager;
    bool draggingBG;
    Vector3 positionOffset;

    void Update()
    {
        if (Utils.IsPointerOverUIObject())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            draggingBG = false;
            Vector3 pos = MappingManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit[] hits;
            hits = Physics.RaycastAll(pos, transform.forward, 100.0F);

            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider.gameObject.tag == "VerticeAngle")
                {
                    mappingManager.VerticeClicked(hit.collider.gameObject.GetComponent< VerticeAngle>());
                    return;
                }
            }
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
                if (hit.collider.gameObject.tag == "Floor" && mappingManager.state != MappingManager.states.SKETCHING)
                {
                    mappingManager.ClickOnFloor(pos);
                }
            }
        } 
    }
}
