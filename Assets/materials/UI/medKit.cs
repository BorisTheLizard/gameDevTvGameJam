using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class medKit : MonoBehaviour
{
	[SerializeField] int healAmount = 3;
	//[SerializeField] AudioClip eatCandySound;

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
				//AudioSource.PlayClipAtPoint(eatCandySound, transform.position);
				Destroy(this.gameObject);
			}
		}
	}
}
