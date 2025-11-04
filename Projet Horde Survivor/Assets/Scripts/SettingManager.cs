using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingManager : MonoBehaviour
{
    //public Image overlayImage;
   // public Slider brightnessSlider;

    public Toggle fullscreenToggle;

    private void Start()
    {
       // brightnessSlider.value = 1f;
       // UpdateBrightness(brightnessSlider.value);
       // brightnessSlider.onValueChanged.AddListener(UpdateBrightness);

        if (fullscreenToggle != null)
            fullscreenToggle.isOn = Screen.fullScreen;

        
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
 
   /* private void UpdateBrightness(float value)
    {
        float alpha = Mathf.Clamp01(1f - value);
        overlayImage.color = new Color(0f, 0f, 0f, alpha);
    }*/
}
