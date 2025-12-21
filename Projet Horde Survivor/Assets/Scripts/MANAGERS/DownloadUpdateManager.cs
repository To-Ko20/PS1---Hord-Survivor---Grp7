using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DownloadUpdateManager : MonoBehaviour
{
    [SerializeField] private List<ulong> updatesSizes = new List<ulong>();
    public ulong downloadMaxSpeed;
    [SerializeField] private ulong downloadSpeed;
    [SerializeField] private int currentUpdate = 0;
    
    [SerializeField] private Slider dlSlider;
    [SerializeField] private Image dlSliderColor;
    [SerializeField] private TMP_Text dlText;
    [SerializeField] private TMP_Text dlRatio;
    [SerializeField] private TMP_Text dlProgression;
    [SerializeField] private TMP_Text dlRemainingTime;

    [SerializeField] private GameObject downloadDisplay;
    [SerializeField] private GameObject unlockDownloadGo;
    [SerializeField] private GameObject maxSpeedGo;
    
    [SerializeField] private GameObject animUI;
    [SerializeField] private GameObject clickUI;
    
    [SerializeField] private GameObject dlSoundTrigger;
    [SerializeField] private GameObject finishedDlSoundTrigger;
    
    private ulong _downloaded;
    private float _t = 1f;

    public bool isDownloading;
    

    public static DownloadUpdateManager Instance;
    
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
        DisplayDownloadInfo();
    }

    public void SwitchDownloading()
    {
        isDownloading = !isDownloading;
        dlSoundTrigger.SetActive(isDownloading);
        DisplayDownloadInfo();
    }
    
    void FixedUpdate()
    {
        if (isDownloading)
        {
            Countdown();
        }
    }

    private void Countdown()
    {
        _t -= Time.fixedDeltaTime;
        
        if (_t <= 0)
        {
            _t= 0.99f;
            Download();
            DisplayDownloadInfo();
        }
    }

    private void Download()
    {
        if (ClickerManager.Instance.bits >= downloadMaxSpeed)
        {
            downloadSpeed = downloadMaxSpeed;
        }
        else
        {
            downloadSpeed = ClickerManager.Instance.bits;
        }

        if (downloadSpeed > 0)
        {
            GameObject newAnimUI = Instantiate(animUI, transform);
            newAnimUI.transform.SetParent(clickUI.transform);
            Animation anim =  newAnimUI.GetComponent<Animation>();
            anim.Play("DownloadAnim");
            StartCoroutine(WaitForAnimation(anim, "DownloadAnim", newAnimUI));
        }
        
        if (_downloaded + downloadSpeed > updatesSizes[currentUpdate])
        {
            downloadSpeed = _downloaded + downloadSpeed - updatesSizes[currentUpdate];
            ClickerManager.Instance.bits -= downloadSpeed;
            _downloaded += downloadSpeed;
            InstallUpdate();
        }
        else if (_downloaded == updatesSizes[currentUpdate])
        {
            ClickerManager.Instance.bits -= downloadSpeed;
            _downloaded += downloadSpeed;
            InstallUpdate();
        }
        else
        {
            ClickerManager.Instance.bits -= downloadSpeed;
            _downloaded += downloadSpeed;
        }
    }
    
    IEnumerator WaitForAnimation(Animation animation, string animName, GameObject animObj)
    {
        // wait for animation to actually start playing
        yield return null;

        AnimationState state = animation[animName];

        // Wait until the animation is done
        while (animation.IsPlaying(animName))
        {
            yield return null;
        }
        Destroy(animObj);
    }

    private void DisplayDownloadInfo()
    {
        if (isDownloading)
        {
            dlSliderColor.color = Color.green;
            dlProgression.color = Color.green;
            dlRatio.color = Color.green;
            dlRemainingTime.color = Color.green;
            dlText.color = Color.green;
        }
        else
        {
            dlSliderColor.color = Color.red;
            dlProgression.color = Color.red;
            dlRatio.color = Color.red;
            dlRemainingTime.color = Color.red;
            dlText.color = Color.red;
        }
        
        ClickerManager.Instance.DisplayUpdate();
        dlSlider.value = _downloaded * 100 /  updatesSizes[currentUpdate];
        dlProgression.text = (_downloaded * 100 / updatesSizes[currentUpdate]).ToString("000.00") + "%";
        dlRatio.text = ClickerManager.Instance.ConvertBits(_downloaded) + " /\n" + ClickerManager.Instance.ConvertBits(updatesSizes[currentUpdate]);
        if (downloadSpeed != 0 && isDownloading)
        {
            dlRemainingTime.text = ((updatesSizes[currentUpdate] - _downloaded) / downloadSpeed).ToString("00:00") + "s";
        }
        else
        {
            dlRemainingTime.text = "99:59s";
        }
    }

    private void ChooseUpdate()
    {
        UpgradeMenuManager.Instance.DisplayUpgradeMenu();
    }

    public void SetChosenUpdate(TMP_Text name)
    {
        dlText.text = name.text;
    }

    private void InstallUpdate()
    {
        SwitchDownloading();
        finishedDlSoundTrigger.SetActive(false);
        finishedDlSoundTrigger.SetActive(true);
        _downloaded = 0;
        currentUpdate++;
        UpgradeMenuManager.Instance.ActivateSkill();
    }

    public void UnlockDownload()
    {
        unlockDownloadGo.SetActive(false);
        maxSpeedGo.SetActive(true);
        downloadDisplay.SetActive(true);
        ChooseUpdate();
    }
}
