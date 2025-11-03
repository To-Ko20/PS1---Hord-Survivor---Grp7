using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManger : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuButton;
    [SerializeField] private RealTimeTypingTextDisplay titleTextDisplay;
    private bool hasToShowButtons;
    private bool buttonAnimationDone = false;
    
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject gameSelectorMenu;
    [SerializeField] private GameObject settingsMenu;

    private void Update()
    {
        if (titleTextDisplay.isMainMenuEndAnimation && !buttonAnimationDone)
        {
            hasToShowButtons = true;
            buttonAnimationDone = true;
            mainMenuButton.GetComponent<CanvasGroup>().DOFade(0f, 0.75f).OnComplete(ShowButtons);
            
        }
    }

    private void ShowButtons()
    {
        mainMenuButton.GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }
    

    public void DisplayMainMenu()
    {
        mainMenu.SetActive(true);
        gameSelectorMenu.SetActive(false);
        settingsMenu.SetActive(false);
        if (hasToShowButtons)
        {
            mainMenuButton.SetActive(true);
        }
    }
    
    public void DisplayGameSelectorMenu()
    {
        mainMenu.SetActive(false);
        gameSelectorMenu.SetActive(true);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("CoreProto");
    }
    
    
    public void DisplaySettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
