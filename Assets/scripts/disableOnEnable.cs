using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class disableOnEnable : MonoBehaviour
{
	public float timeToWait=3f;

	public bool isTmpText;
	[SerializeField] TMP_Text[] textLayers;
	

	private void OnEnable()
	{
		StartCoroutine(counter());
	}
	public IEnumerator counter()
	{
		yield return new WaitForSeconds(timeToWait);
		if (isTmpText)
		{
			foreach (var item in textLayers)
			{
				item.text = "";
			}
		}
		this.gameObject.SetActive(false);
	}
}
