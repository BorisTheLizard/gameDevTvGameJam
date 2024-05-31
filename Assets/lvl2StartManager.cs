using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl2StartManager : MonoBehaviour
{
	[SerializeField] GameObject path;
	[SerializeField] GameObject timeline;
	[SerializeField] GameObject screenCut;
	public bool canSkip =false;

	private void Start()
	{
		StartCoroutine(disablePath());
	}
	IEnumerator disablePath()
	{
		yield return new WaitForSeconds(0.5f);
		path.SetActive(false);
		yield return new WaitForSeconds(0.5f);
		canSkip = true;
	}
	public void startScene2()
	{
		if (canSkip)
		{
			path.SetActive(true);
			screenCut.SetActive(false);
			timeline.SetActive(false);
			this.enabled = false;
		}
	}
	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			startScene2();
		}
	}
}
