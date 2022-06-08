using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Toolbar : MonoBehaviour
{
    public List<GameObject> elements;
    public SettingsBinder Binder;

    public int page = 0;

    public void SetPage(int pg)
    {
        Binder.ResetPreviewPane();
        elements[page].SetActive(false);
        page = pg;
        elements[page].SetActive(true);
    }
}