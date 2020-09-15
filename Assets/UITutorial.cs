using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UITutorial : MonoBehaviour
{
    public GameObject panel;
    public TutorialData[] all;

    public enum types   {
        MEDIDAS
    }

    [Serializable]
    public class TutorialData
    {
        public GameObject panel;
        public types type;
    }
    System.Action OnDone;
    void Start()
    {
        panel.SetActive(false);
        Events.OnTutorial += OnTutorial;
    }
    void OnDestroy()
    {
        Events.OnTutorial -= OnTutorial;
    }
    void OnTutorial(types type, System.Action OnDone)
    {
        panel.SetActive(true);
        this.OnDone = OnDone;
        Reset();
        GetTutorial(type).panel.SetActive(true);
    }
    TutorialData GetTutorial(types type)
    {
        foreach (TutorialData tutorialData in all)
            if (tutorialData.type == type)
                return tutorialData;
        return null;
    }    
    private void Reset()
    {
        foreach (TutorialData tutorialData in all)
            tutorialData.panel.SetActive(false);
    }
    public void Done()
    {
        if(OnDone != null)
            OnDone();
        panel.SetActive(false);
    }
}
