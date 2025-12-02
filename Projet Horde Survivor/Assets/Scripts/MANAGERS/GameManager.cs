using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private KeyCode menuKey = KeyCode.Escape;
    [SerializeField] private bool isMenuOpen = false;
    public bool isPlaying = true;


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

    void Start()
    {
        isPlaying = true;
        Time.timeScale = 1;
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
	
	/*public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
	}*/

    public void GameOver()
    {
        isPlaying = false;
        menuUI.SetActive(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void Win()
    {
        isPlaying = false;
        menuUI.SetActive(false);
        winUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
