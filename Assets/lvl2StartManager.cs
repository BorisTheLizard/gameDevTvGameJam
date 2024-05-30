using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl2StartManager : MonoBehaviour
{
	[SerializeField] GameObject path;
	[SerializeField] GameObject timeline;
	[SerializeField] GameObject screenCut;

	private void Start()
	{
		StartCoroutine(disablePath());
	}
	IEnumerator disablePath()
	{
		yield return new WaitForSeconds(0.5f);
		path.SetActive(false);
	}
	public void startScene2()
	{
		timeline.SetActive(false);
		path.SetActive(true);
		screenCut.SetActive(false);
		this.enabled = false;
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			startScene2();
		}
	}
}
