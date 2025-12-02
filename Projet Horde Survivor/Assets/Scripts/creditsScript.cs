using UnityEngine;

public class creditsScript : MonoBehaviour
{
    public float scrollSpeed  = 40f;
    public float endPositionY = 1580f;
    public GameObject creditsMenu;
    public GameObject mainMenu;
    
    private RectTransform rectTransform;
	private Vector2 startPosition = new Vector2(0, -1570f);


    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

		rectTransform.anchoredPosition = startPosition;
    }

	    void OnEnable()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();

        rectTransform.anchoredPosition = startPosition;
    }
    
    void Update()
    {
        rectTransform.anchoredPosition += new Vector2(0, scrollSpeed * Time.deltaTime);

        if (rectTransform.anchoredPosition.y >= endPositionY)
        {
           CloseCredits();
        }
    }
	
	void CloseCredits()
    {
        creditsMenu.SetActive(false);
		mainMenu.SetActive(true);

	rectTransform.anchoredPosition = startPosition;
    }

}

