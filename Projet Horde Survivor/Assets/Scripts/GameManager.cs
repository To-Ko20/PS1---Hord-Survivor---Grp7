using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private bool isMenuOpen = false;
    
    
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
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

    public void GameOver()
    {
        Time.timeScale = 0;
        menuUI.SetActive(false);
        gameOverUI.SetActive(true);
    }

    public void Win()
    {
        Time.timeScale = 0;
        menuUI.SetActive(false);
        winUI.SetActive(true);  
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("First Playable");
        Time.timeScale = 1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
