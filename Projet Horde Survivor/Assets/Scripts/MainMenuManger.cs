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
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject creditsMenu;

    private void Update()
    {
        if (titleTextDisplay.isMainMenuEndAnimation && !buttonAnimationDone)
        {
            hasToShowButtons = true;
            buttonAnimationDone = true;
            mainMenuButton.GetComponent<CanvasGroup>().DOFade(0f, 0.75f).OnComplete(ShowButtons);
            
        }
        else if (!titleTextDisplay.isMainMenuEndAnimation && buttonAnimationDone){}
    }

    private void ShowButtons()
    {
        mainMenuButton.GetComponent<CanvasGroup>().DOFade(1f, 1f);
    }
    

    public void DisplayMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        creditsMenu.SetActive(false);
        if (hasToShowButtons)
        {
            mainMenuButton.SetActive(true);
        }
    }

    public void PlayGame()
    {
        DOTween.Clear(true);
        SceneManager.LoadScene(1);
    }
    
    
    public void DisplaySettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }
    
    public void DisplayCreditsMenu()
    {
        mainMenu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    
    public void Quit()
    {
        Application.Quit();
    }
}
