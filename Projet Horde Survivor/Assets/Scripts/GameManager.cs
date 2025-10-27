using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private bool isMenuOpen = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(menuKey))
        {
            ChangeMenuState();
        }
    }
    
    public void ChangeMenuState()
    {
        isMenuOpen = !isMenuOpen;
        menuUI.SetActive(isMenuOpen);

        if (isMenuOpen)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("CoreProto");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
