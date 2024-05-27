using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyObjEffectPushRB : MonoBehaviour
{
	[SerializeField] float radius = 5f;
	[SerializeField] LayerMask destructableLayer;
	[SerializeField] float power = 10;

	private void OnEnable()
	{
		StartCoroutine(pushWait());

		IEnumerator pushWait()
		{
			yield return new WaitForSeconds(0.15f);

			Collider[] colliders = Physics.OverlapSphere(transform.position, radius, destructableLayer);

			foreach (var item in colliders)
			{
				if (item.GetComponent<Rigidbody>() != null)
				{
					item.GetComponent<Rigidbody>().AddExplosionForce(power, transform.position, radius + 2,0, ForceMode.Impulse);
				}
			}
		}
	}
}
