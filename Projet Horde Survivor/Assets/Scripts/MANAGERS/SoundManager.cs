using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject clickTrigger;
    [SerializeField] private GameObject buyTrigger;
    public GameObject musicTrigger;
    
    public static SoundManager Instance;

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
    void Start ()
    {
        var buttons = FindObjectsByType<Button>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (var b in buttons)	{		
            UnityAction l = delegate { OnClick(); };
            b.onClick.AddListener(l);
        }
    }
    

    void OnClick()
    {
        clickTrigger.SetActive(!clickTrigger.activeSelf);
    }
    

    public void PlayBuy()
    {
        buyTrigger.SetActive(!buyTrigger.activeSelf);
    }
}
