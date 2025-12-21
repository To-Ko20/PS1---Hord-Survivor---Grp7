using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    private FMOD.Studio.VCA _vcaController;
    [SerializeField] private string _vcaName;

    void Start()
    {
        _vcaController = FMODUnity.RuntimeManager.GetVCA("vca:/" + _vcaName);
        SetVolume(GetComponent<Slider>());
    }

    public void SetVolume(Slider volume)
    {
        _vcaController.setVolume(volume.value);
    }
}
