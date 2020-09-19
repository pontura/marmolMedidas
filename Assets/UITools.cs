using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITools : MonoBehaviour
{
    public GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }
    public void Init()
    {
        panel.SetActive(true);
    }
    public void DeleteAll()
    {
        GetComponent<UIIntro>().Init();
        Events.DeleteAll();
    }
}
