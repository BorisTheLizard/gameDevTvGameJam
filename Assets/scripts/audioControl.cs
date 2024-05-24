using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.IO;

public class audioControl : MonoBehaviour
{
	public AudioMixer mixer;

	[SerializeField] Slider masterSlider;
	[SerializeField] Slider musicSlider;
	[SerializeField] Slider sfxSlider;

	private void Start()
	{
		//set music
		musicSlider.onValueChanged.AddListener(setMusicVolume);
		if (!PlayerPrefs.HasKey("musicVolume")) // Check if music volume is not set yet
		{
			PlayerPrefs.SetFloat("musicVolume", 1f); // Set music volume to 1 if not set
		}
		mixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("musicVolume"));
		musicSlider.value = PlayerPrefs.GetFloat("musicVolume");

		//set sfx
		sfxSlider.onValueChanged.AddListener(setSfxVolume);
		if (!PlayerPrefs.HasKey("sfxVolume")) // Check if sfx volume is not set yet
		{
			PlayerPrefs.SetFloat("sfxVolume", 1f); // Set sfx volume to 1 if not set
		}
		mixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sfxVolume"));
		sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume");

		//set master
		masterSlider.onValueChanged.AddListener(setMasterVolume);
		if (!PlayerPrefs.HasKey("masterVolume")) // Check if master volume is not set yet
		{
			PlayerPrefs.SetFloat("masterVolume", 1f); // Set master volume to 1 if not set
		}
		mixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("masterVolume"));
		masterSlider.value = PlayerPrefs.GetFloat("masterVolume");

		//CreateTxtFileOnDesktop();
	}

	public void setMusicVolume(float value)
	{
		mixer.SetFloat("musicVolume", Mathf.Log10(value)*20);
		PlayerPrefs.SetFloat("musicVolume", value);
		PlayerPrefs.Save();
	}
	public void setSfxVolume(float value)
	{
		mixer.SetFloat("sfxVolume", Mathf.Log10(value) * 20);
		PlayerPrefs.SetFloat("sfxVolume", value);
		PlayerPrefs.Save();
	}
	public void setMasterVolume(float value)
	{
		mixer.SetFloat("masterVolume", Mathf.Log10(value) * 20);
		PlayerPrefs.SetFloat("masterVolume", value);
		PlayerPrefs.Save();
	}
}
