using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
	float shootingSpeed;
	[SerializeField] float maxShootingSpeed = 0.5f;
	GameObjectPool pool;
	[SerializeField] int objectIndex;
	public int bulletsInClip = 5;
	[SerializeField] Transform shootingPoint;

	AudioSource audioSource;
	[SerializeField] AudioClip click;
	[SerializeField] AudioClip Shoot;



	[SerializeField] float noiseRadius=20;
	[SerializeField] LayerMask enemyLayer;

	private void Start()
	{
		pool = FindObjectOfType<GameObjectPool>();
		audioSource = GetComponent<AudioSource>();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			shooting();
		}
		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			//reload
			Debug.Log("Reload");
		}
	}
	private void shooting()
	{
		if (Time.time > shootingSpeed)
		{
			if (bulletsInClip > 0)
			{
				//muzzleFlash.Play();
				shootingSpeed = Time.time + maxShootingSpeed;
				//audioSource.pitch = Random.Range(0.9f, 1.1f);
				//audioSource.PlayOneShot(Shoot);
				GameObject obj = pool.GetObject(objectIndex);
				if (obj != null)
				{
					obj.SetActive(false);
					obj.transform.position = shootingPoint.position;
					obj.transform.rotation = shootingPoint.rotation;
					obj.SetActive(true);
				}
				bulletsInClip--;
				makeNoise();
			}
			else
			{
				audioSource.PlayOneShot(click);
			}
		}
	}
	private void makeNoise()
	{
		Collider[] col = Physics.OverlapSphere(transform.position, noiseRadius, enemyLayer);

		foreach (var item in col)
		{
			if (item.GetComponent<topDownAI>() != null)
			{
				item.GetComponent<topDownAI>().heardNoise();
			}
		}
	}
}
