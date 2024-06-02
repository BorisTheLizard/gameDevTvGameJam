using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialText : MonoBehaviour
{
	timeController _time;
	[SerializeField] GameObject tutorial;
	private void Start()
	{
		_time = FindObjectOfType<timeController>();
		StartCoroutine(showTut());
	}

	IEnumerator showTut()
	{
		yield return new WaitForSeconds(1);
		tutorial.SetActive(true);
		_time.setTime(0);
	}
	public void closeTut()
	{
		_time.setTime(1);
		tutorial.SetActive(false);
	}
}
