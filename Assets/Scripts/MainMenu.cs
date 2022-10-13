using MoreMountains.TopDownEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject playerDeadCanvas;
    [SerializeField]
    GameObject mainUIDeadCanvas;
    [SerializeField]
    GameObject hudUI;
    [SerializeField]
    GameObject howToPlayUI;
    [SerializeField]
    GameObject mainMenuUI;
    private void OnEnable()
    {
        Health.playerDead += PlayerDied;
    }

    private void OnDisable()
    {
        Health.playerDead -= PlayerDied;
    }

    public void BeginGame()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void PlayerDied()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        mainUIDeadCanvas.SetActive(false);
        playerDeadCanvas.SetActive(true);
        hudUI.SetActive(false);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void BackToMainUI()
    {
        mainMenuUI.SetActive(true);
        howToPlayUI.SetActive(false);
    }
    public void HowToPlay()
    {
        howToPlayUI.SetActive(true);
        mainMenuUI.SetActive(false);
    }
}
