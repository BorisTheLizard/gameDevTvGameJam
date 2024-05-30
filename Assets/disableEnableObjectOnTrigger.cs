using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class disableEnableObjectOnTrigger : MonoBehaviour
{
	public bool disableIt;
	[SerializeField] GameObject objectToDisable;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			objectToDisable.SetActive(disableIt);
		}
	}
}
