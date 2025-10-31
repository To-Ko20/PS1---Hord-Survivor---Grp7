using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DownloadUpdateManager : MonoBehaviour
{
    [SerializeField] private List<ulong> updatesSizes = new List<ulong>();
    [SerializeField] private ulong downloadMaxSpeed;
    [SerializeField] private ulong downloadSpeed;
    [SerializeField] private int currentUpdate = 0;
    
    [SerializeField] private Slider dlSlider;
    [SerializeField] private TMP_Text dlRatio;
    [SerializeField] private TMP_Text dlProgression;
    [SerializeField] private TMP_Text dlRemainingTime;
    
    private ulong _downloaded;
    private float _t = 1f;
    

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
    
    void FixedUpdate()
    {
        Countdown();
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

    private void DisplayDownloadInfo()
    {
        ClickerManager.Instance.DisplayUpdate();
        dlSlider.value = _downloaded * 100 /  updatesSizes[currentUpdate];
        dlProgression.text = (_downloaded * 100 / updatesSizes[currentUpdate]).ToString("000.00") + "%";
        dlRatio.text = ClickerManager.Instance.ConvertBits(_downloaded) + " /\n" + ClickerManager.Instance.ConvertBits(updatesSizes[currentUpdate]);
        if (downloadSpeed != 0)
        {
            dlRemainingTime.text = ((updatesSizes[currentUpdate] - _downloaded) * downloadSpeed).ToString("00:00") + "s";
        }
        else
        {
            dlRemainingTime.text = "99:59s";
        }
    }

    private void InstallUpdate()
    {
        _downloaded = 0;
        currentUpdate++;
        Debug.Log("Update downloaded and installed");
    }
}
