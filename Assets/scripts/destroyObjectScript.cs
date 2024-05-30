using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class destroyObjectScript : MonoBehaviour
{
	[SerializeField] GameObject[] destructParts;
	[SerializeField] GameObject[] bodyPart;
	bool isCounting;

	public void destroyIt()
	{
		this.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
		foreach (var item in bodyPart)
		{
			item.SetActive(false);
		}

		foreach (var item in destructParts)
		{
			item.GetComponent<Rigidbody>().isKinematic = false;
			item.GetComponent<Rigidbody>().useGravity = true;
		}

		if (!isCounting)
		{
			isCounting = true;
			StartCoroutine(countTosettle());

			IEnumerator countTosettle()
			{
				yield return new WaitForSeconds(7f);
				foreach (var item in destructParts)
				{
					item.GetComponent<Rigidbody>().isKinematic = !false;
					item.GetComponent<Rigidbody>().useGravity = !true;
					item.GetComponent<BoxCollider>().isTrigger = true;
					this.enabled = false;
				}
			}
		}
	}
}
