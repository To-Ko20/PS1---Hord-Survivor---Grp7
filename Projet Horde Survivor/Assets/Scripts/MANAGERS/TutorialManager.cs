using UnityEngine;
using UnityEngine.InputSystem.HID;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject     tutorialFrame;
    [SerializeField] private GameObject     closeTutorial;
    [SerializeField] private GameObject     mainButton;
    [SerializeField] private GameObject     quitNoButton;
    [SerializeField] private GameObject     quitYesButton;
    
    public                   int            popUpIndex = 0;
    public                   GameObject[]   popUps;
    
    [SerializeField] private Transform      tutorialDataBubble;
    [SerializeField] private GameObject     wallToDestroy;
    
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private ClickerManager clickerManager;
    [SerializeField] private GameObject     enemySpawner;
    
    public static TutorialManager Instance;

    private void Awake()
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
        closeTutorial.SetActive(false);
        
        mainButton.SetActive(false);
        quitYesButton.SetActive(false);
        quitNoButton.SetActive(false);
        
        enemySpawner.SetActive(false);
    }

    void Update()
    {
        for (int i = 0; i < popUps.Length; i++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
                
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        
        if (popUpIndex == 0)
        {
            PlayerController.Instance.canMove = false;
        }
        
        else if (popUpIndex == 3)
        {
            PlayerController.Instance.canMove = false;
            cameraMovement.cameraTarget = tutorialDataBubble;
        }
        
        else if (popUpIndex == 4)
        {
            PlayerController.Instance.canMove = true;
            cameraMovement.cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
            
            if (clickerManager.bits >= 8)
            {
                NextPopUp();
            }
        }
        
        else if (popUpIndex == 5)
        {
            PlayerController.Instance.canMove = false;
        }
        
        else if (popUpIndex == 6)
        {
            PlayerController.Instance.canMove = false;
        }
        
        else if (popUpIndex == 7)
        {
            PlayerController.Instance.canMove = false;
        }
        
        else if (popUpIndex == 8)
        {
            PlayerController.Instance.canMove = false;
        }
        
        else if (popUpIndex == 9)
        {
            PlayerController.Instance.canMove = false;
        }
        
        else
        {
            PlayerController.Instance.canMove = true;
            cameraMovement.cameraTarget = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }
    
    public void NextPopUp()
    {
        popUpIndex++;
        mainButton.SetActive(false);
    }

    public void ToggleMainButton()
    {
        mainButton.SetActive(true);
    }

    public void ToggleAuxiliaryButtons()
    {
        quitYesButton.SetActive(true);
        quitNoButton.SetActive(true);
    }

    public void EndTutorial()
    {
        tutorialFrame.SetActive(false);
        closeTutorial.SetActive(true);
    }

    public void ReturnToTuturial()
    {
        tutorialFrame.SetActive(true);
        closeTutorial.SetActive(false);
    }

    public void EndTutorialConfirm()
    {
        Debug.Log("Confirm");
    }
}