using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subtitleDebugTrigger : MonoBehaviour
{
	printSubtitles _printSubtitles;
	public int textIndex;

	private void Awake()
	{
		_printSubtitles = FindObjectOfType<printSubtitles>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_printSubtitles.showSubtitles(textIndex);
			this.gameObject.GetComponent<BoxCollider>().enabled = false;
			StartCoroutine(disableTrig());
		}
	}
	IEnumerator disableTrig()
	{
		yield return new WaitForSeconds(2);
		this.gameObject.GetComponent<BoxCollider>().enabled = !false;
	}
}
