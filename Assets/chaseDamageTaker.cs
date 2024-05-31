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
	[SerializeField] GameObject hitImpact;
	[SerializeField] AudioSource _audioSource;
	[SerializeField] AudioClip collisionClip;


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
			other.GetComponent<destroyObjectScript>().gameObject.GetComponent<BoxCollider>().enabled = false;
			anim.SetTrigger("collision");
			hitImpact.SetActive(true);
			StartCoroutine(disableHitImpact());
			_audioSource.PlayOneShot(collisionClip);

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
	IEnumerator disableHitImpact()
	{
		yield return new WaitForSeconds(1);
		hitImpact.SetActive(false);
	}

}
