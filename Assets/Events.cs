using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.XR.ARFoundation;

public static class Events
{
    public static System.Action<GameObject, GameObject, string> OnAddDistanceSignal = delegate { };
    //public static System.Action<VerticeAngle, VerticeAngle> OnAddAngle = delegate { };
    public static System.Action ReCalculateAll = delegate { };
    public static System.Action DeleteAll = delegate { };
    public static System.Action<UITutorial.types, System.Action> OnTutorial = delegate { };
    
}
