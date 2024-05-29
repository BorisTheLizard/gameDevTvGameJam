using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medKit : MonoBehaviour
{
	[SerializeField] int healAmount = 3;
	[SerializeField] AudioClip eatSound;

	private void OnTriggerEnter(Collider other)
	{
		if(other.tag=="Player")
		{
			if(other.GetComponent<healthSystem>().Health< other.GetComponent<healthSystem>().MaxHealth)
			{
				other.GetComponent<healthSystem>().RestoreHealth(healAmount);
				if(other.GetComponent<healthSystem>().Health> other.GetComponent<healthSystem>().MaxHealth)
				{
					other.GetComponent<healthSystem>().Health = other.GetComponent<healthSystem>().MaxHealth;
				}
				other.GetComponent<AudioSource>().PlayOneShot(eatSound);
				Destroy(this.gameObject);
			}
		}
	}
}
