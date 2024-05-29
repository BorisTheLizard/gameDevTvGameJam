using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.Audio;

public class timeController : MonoBehaviour
{
	public bool isSlowTime = false;
	[SerializeField] Volume mainEffect;
	[SerializeField] Volume slowTimeEffect;
	[SerializeField] float effectChangeSpeed = 0.5f;
	public bool GamePaused = false;

	public float energy;
	public float MaxEnergy=100f;

	AudioMixer mixer;
	[SerializeField] audioControl _audioControl;

	private void Start()
	{
		energy = MaxEnergy;
		mixer = _audioControl.mixer;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			if (!isSlowTime)
			{
				setTime(0.3f);
				isSlowTime = true;
			}
			else
			{
				setTime(1f);
				isSlowTime = false;
			}
		}
	}

	private void FixedUpdate()
	{
		if (isSlowTime)
		{
			energy-= 15 * Time.deltaTime;
			mixer.SetFloat("sfxPitch", Time.timeScale = 0.3f);
			if (energy <= 0)
			{
				energy = 0;
				isSlowTime = false;
				setTime(1f);
			}
			//effect
			if (slowTimeEffect.weight < 1)
			{
				slowTimeEffect.weight = Mathf.Lerp(slowTimeEffect.weight, 1, effectChangeSpeed);
				mainEffect.weight = Mathf.Lerp(mainEffect.weight, 0, effectChangeSpeed);
			}
		}
		else
		{
			if (mainEffect.weight < 1)
			{
				slowTimeEffect.weight = Mathf.Lerp(slowTimeEffect.weight, 0, effectChangeSpeed);
				mainEffect.weight = Mathf.Lerp(mainEffect.weight, 1, effectChangeSpeed);
			}
			mixer.SetFloat("sfxPitch", Time.timeScale = 1f);
		}
	}

	public void setTime(float timeToSet)
	{
		if (timeToSet == 0)
		{
			GamePaused = true;
		}
		if (timeToSet > 0.05f)
		{
			GamePaused = !true;
		}
		Time.timeScale = timeToSet;
	}
	public void addEnergy()
	{
		if (energy < MaxEnergy)
		{
			energy += 5;
		}
		if (energy > MaxEnergy)
		{
			energy = MaxEnergy;
		}
	}
}
