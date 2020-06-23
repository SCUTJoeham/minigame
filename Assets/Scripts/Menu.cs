using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

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
    }

    public void ClearDnaMenu()
    {
        pauseDialog.SetActive(false);
        promptDialog.SetActive(false);
        dnaMenu.SetActive(false);
    }

    public void DisplayPauseMenu()
    {
        promptDialog.SetActive(false);
        dnaMenu.SetActive(false);
        pauseDialog.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseDialog.SetActive(false);
        promptDialog.SetActive(false);
        dnaMenu.SetActive(false);
    }

    public void DisplayPromptPanel()
    {
        dnaMenu.SetActive(false);
        pauseDialog.SetActive(false);
        promptDialog.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }
}
