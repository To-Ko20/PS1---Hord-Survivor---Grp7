using UnityEngine;
using UnityEngine.InputSystem.HID;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialFrame;
    [SerializeField] private GameObject closeTutorial;
    
    public GameObject[] popUps;
    public int popUpIndex;
    [SerializeField] private GameObject button;
    [SerializeField] private Transform tutorialDataBubble;
    
    [SerializeField] private GameObject button1;
    [SerializeField] private GameObject button2;
    
    [SerializeField] private CameraMovement cameraMovement;
    [SerializeField] private ClickerManager clickerManager;
    
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
        button.SetActive(false);
        button1.SetActive(false);
        button2.SetActive(false);
        closeTutorial.SetActive(false);
        popUpIndex = 0;
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
        button.SetActive(false);
    }

    public void ToggleButton()
    {
        button.SetActive(true);
    }

    public void ToggleTutorialOverButtons()
    {
        button1.SetActive(true);
        button2.SetActive(true);
    }

    public void EndTutorial()
    {
        tutorialFrame.SetActive(false);
        closeTutorial.SetActive(true);
    }

    public void Return()
    {
        tutorialFrame.SetActive(true);
        closeTutorial.SetActive(false);
    }

    public void Confirm()
    {
        Debug.Log("Confirm");
    }
}