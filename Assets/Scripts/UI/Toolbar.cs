using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Toolbar : MonoBehaviour
{
    public List<GameObject> elements;

    public int page;

    public void SetPage(int pg)
    {
        elements[page].SetActive(false);
        page = pg;
        elements[page].SetActive(true);
    }
}