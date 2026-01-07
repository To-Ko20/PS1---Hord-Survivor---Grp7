using FMODUnity;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject winUI;
    [SerializeField] private KeyCode    menuKey    = KeyCode.Escape;
    [SerializeField] private bool       isMenuOpen = false;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private bool       isInSettings = false;
    public                   bool       isPlaying    = true;

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
        if (Time.timeScale == 0)
        {
            RuntimeManager.StudioSystem.setParameterByName("Game Status", 1);
        }
        else
        {
            RuntimeManager.StudioSystem.setParameterByName("Game Status", 0);
        }
        
        if (isInSettings)
            return;

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
            //SoundManager.Instance.musicSource.volume = 0.2f;
            //SoundManager.Instance.musicSource.reverbZoneMix = 1.1f;
            //SoundManager.Instance.musicSource.pitch = 0.90f;
        }
        else
        {
            Time.timeScale = 1;
            //SoundManager.Instance.musicSource.volume = 1f;
            //SoundManager.Instance.musicSource.reverbZoneMix = 1f;
            //SoundManager.Instance.musicSource.pitch = 1f;
        }
    }
	
	public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
	}

    public void DisplaySettingsMenu()
    {
        isMenuOpen = false;
        menuUI.SetActive(false);
        
        settingsMenu.SetActive(true);
        isInSettings   = true;
        Time.timeScale = 0;
    }
    
    public void CloseSettingsMenu()
    {
        settingsMenu.SetActive(false);
        isInSettings = false;
        
        isMenuOpen = true;
        menuUI.SetActive(true);
    }

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
