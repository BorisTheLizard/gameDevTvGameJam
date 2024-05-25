using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timeController : MonoBehaviour
{
	public bool isSlowTime = false;


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

	public void setTime(float timeToSet)
	{
		Time.timeScale = timeToSet;
	}
}
