using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;

public class SettingManager : MonoBehaviour
{
	
	public AudioMixer audioMixer;
	public void SetVolume(float volume)
	{
		audioMixer.SetFloat("volume", volume);
	} 
	
	
	public void SetFullscreen(bool isFullscreen)
	{
		Screen.fullScreen = isFullscreen;
	}
}    
