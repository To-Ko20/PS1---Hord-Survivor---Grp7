using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private GameObject buyTrigger;
    
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

    public void PlayBuy()
    {
        buyTrigger.SetActive(!buyTrigger.activeSelf);
    }
}
