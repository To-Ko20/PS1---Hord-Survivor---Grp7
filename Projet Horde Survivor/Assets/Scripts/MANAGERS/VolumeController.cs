using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VolumeController : MonoBehaviour, IPointerUpHandler
{
    private FMOD.Studio.VCA _vcaController;

    [SerializeField] private string _vcaName;
    [SerializeField] private Slider slider;

    private void Awake()
    {
        _vcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + _vcaName);
    }

    private void Start()
    {
        // Apply saved value when this controller appears
        float savedValue = GetSavedVolume();
        savedValue = Mathf.Clamp01(savedValue);

        slider.SetValueWithoutNotify(savedValue);
        _vcaController.setVolume(savedValue);

        // Live update while dragging (no save)
        slider.onValueChanged.AddListener(OnSliderValueChanged);
    }

    private void OnSliderValueChanged(float value)
    {
        value = Mathf.Clamp01(value);

        _vcaController.setVolume(value);
        SetSavedVolume(value);
    }

    // Save only when user releases the slider
    public void OnPointerUp(PointerEventData eventData)
    {
        SaveManager.Instance.SaveToJson();
    }

    private float GetSavedVolume()
    {
        var v = SaveManager.Instance.inventory.volumes;

        return _vcaName switch
        {
            "Master" => v.Master,
            "Music"  => v.Music,
            "SFX"    => v.SFX,
            _        => 1f
        };
    }

    private void SetSavedVolume(float value)
    {
        var v = SaveManager.Instance.inventory.volumes;

        switch (_vcaName)
        {
            case "Master": v.Master = value; break;
            case "Music":  v.Music  = value; break;
            case "SFX":    v.SFX    = value; break;
        }
    }
}