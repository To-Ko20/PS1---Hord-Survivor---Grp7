using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private FMOD.Studio.VCA _vcaController;
    [SerializeField] private string _vcaName;
    [SerializeField] private Slider slider;

    void Start()
    {
        _vcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + _vcaName);
        slider.value = SaveManager.Instance.inventory.volumes[_vcaName];
    }

    public void SetVolume(Slider volume)
    {
        _vcaController.setVolume(volume.value);
        SaveManager.Instance.inventory.volumes[_vcaName] = volume.value;
        SaveManager.Instance.SaveToJson();
    }
}
