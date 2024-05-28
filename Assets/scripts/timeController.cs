using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class timeController : MonoBehaviour
{
	public bool isSlowTime = false;
	[SerializeField] Volume mainEffect;
	[SerializeField] Volume slowTimeEffect;
	[SerializeField] float effectChangeSpeed = 0.5f;
	public bool GamePaused = false;

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
}
