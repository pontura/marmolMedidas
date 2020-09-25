using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public MappingManager mappingManager;
    public Vector3 orientation = new Vector3(0,0,1);
    public float originalRaycastPos = -10;

    void Update()
    {
        if (Utils.IsPointerOverUIObject())
            return;
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = MappingManager.Instance.cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z += originalRaycastPos;
            RaycastHit2D [] hits2d;
            
            hits2d = Physics2D.RaycastAll(pos, orientation, 100.0F);
            print("hits2d.Length: " + hits2d.Length);
            for (int i = 0; i < hits2d.Length; i++)
            {
                RaycastHit2D hit = hits2d[i];
                print("choca con : " + hit.collider.gameObject.tag);
            }

            RaycastHit[] hits;
            hits = Physics.RaycastAll(pos, transform.forward, 100.0F);
            
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit hit = hits[i];
               // print("choca con : " + hit.collider.gameObject.tag);
                if (hit.collider.gameObject.tag == "VerticeAngle")
                {
                    mappingManager.VerticeClicked(hit.collider.gameObject.GetComponent< VerticeAngle>());
                    return;
                }
                else if (hit.collider.gameObject.tag == "Floor" && mappingManager.state == MappingManager.states.SKETCHING)
                {
                   // print("b");
                    mappingManager.ClickOnFloor(pos);
                }
            }
        } 
    }
}
