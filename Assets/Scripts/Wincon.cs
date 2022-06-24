using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wincon : MonoBehaviour
{
    public GameObject winpanel;
    private void OnTriggerEnter(Collider other)
    {
        Cursor.lockState = CursorLockMode.None;
        winpanel.SetActive(true);

    }
}
