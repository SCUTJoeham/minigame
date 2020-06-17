using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    // 用作问号面板的打开和关闭
    private int count = 0;

    // dna panel
    public GameObject dnaMenu;

    // pause dialog
    public GameObject pauseDialog;

    // prompt panel
    public GameObject promptDialog;

    private void Start()
    {
        dnaMenu.SetActive(false);
        pauseDialog.SetActive(false);
        promptDialog.SetActive(false);
    }

    public void DisplayDnaMenu()
    {
        pauseDialog.SetActive(false);
        promptDialog.SetActive(false);
        dnaMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ClearDnaMenu()
    {
        pauseDialog.SetActive(false);
        promptDialog.SetActive(false);
        dnaMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void DisplayPauseMenu()
    {
        pauseDialog.SetActive(true);
        promptDialog.SetActive(false);
        dnaMenu.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ClearPauseMenu()
    {
        pauseDialog.SetActive(false);
        promptDialog.SetActive(false);
        dnaMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PromptPanel()
    {
        if (count % 2 == 0)
        {
            dnaMenu.SetActive(false);
            pauseDialog.SetActive(false);
            promptDialog.SetActive(true);
        }
        else
        {
            promptDialog.SetActive(false);
        }
        count++;
    }
}
