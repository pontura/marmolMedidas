using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticeAngle : MonoBehaviour
{
    public int id;
    public float angle;
    public float distance;
    public bool isLastAngle;
    public GameObject asset;

    public void Init(bool isLastAngle = false)
    {
        this.isLastAngle = isLastAngle;
        if (isLastAngle)
        {
            asset.SetActive(false);
            GetComponent<Collider>().enabled = false;
        }
    }
}
