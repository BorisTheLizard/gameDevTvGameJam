using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class chaseDamageTaker : MonoBehaviour
{
	[SerializeField] healthSystem _health;
	[SerializeField] Animator anim;
	GameObjectPool pool;
	[SerializeField] CinemachineImpulseSource _source;

	private void Awake()
	{
		pool = FindObjectOfType<GameObjectPool>();
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "destr")
		{
			_source.GenerateImpulse();
			_health.TakeDamage(1);
			other.GetComponent<destroyObjectScript>().destroyIt();

			GameObject destrObjEffect = pool.GetObject(1);
			if (destrObjEffect != null)
			{
				destrObjEffect.SetActive(false);
				destrObjEffect.transform.position = transform.position;

				destrObjEffect.SetActive(true);
			}
			other.transform.gameObject.GetComponent<destroyObjectScript>().gameObject.GetComponent<BoxCollider>().enabled = false;

		}
	}

}
