using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public GameObject dnaMenu;

    private void Start()
    {
        dnaMenu.SetActive(false);
    }

    public void displayDnaMenu()
    {
        dnaMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void clearDnaMenu()
    {
        dnaMenu.SetActive(false);
        Time.timeScale = 1f;
    }
}
