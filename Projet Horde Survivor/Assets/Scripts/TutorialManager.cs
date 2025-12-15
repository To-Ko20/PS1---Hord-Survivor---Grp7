using UnityEngine;
using UnityEngine.InputSystem.HID;

public class TutorialManager : MonoBehaviour
{
    public           GameObject[] popUps;
    private          int          popUpIndex;
    [SerializeField] GameObject   button;
    
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
        else
        {
            PlayerController.Instance.canMove = true;
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

    public void EndTutorial()
    {
        Debug.Log("EndTutorial");
    }
}