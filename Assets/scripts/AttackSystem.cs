using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class AttackSystem : MonoBehaviour
{
	float shootingSpeed;
	[SerializeField] float maxShootingSpeed = 0.5f;
	GameObjectPool pool;
	[SerializeField] int objectIndex;
	public int bulletsInClip = 5;
	public int maxBulletsInClip = 6;
	[SerializeField] Transform shootingPoint;

	AudioSource audioSource;
	[SerializeField] AudioClip click;
	[SerializeField] AudioClip Shoot;
	[SerializeField] AudioClip reloadSound;


	[SerializeField] float noiseRadius=20;
	[SerializeField] LayerMask enemyLayer;
	[SerializeField] CinemachineImpulseSource _source;

	bool isReloading;

	public bool isCooldown = false;
	public float cooldownTime = 10f;
	private float cooldownTimer = 0f;
	[SerializeField] Image cooldownImage;

	private void Start()
	{
		bulletsInClip = maxBulletsInClip;
		pool = FindObjectOfType<GameObjectPool>();
		audioSource = GetComponent<AudioSource>();
	}

	private void StartCooldown()
	{
		isCooldown = true;
		cooldownTimer = cooldownTime;
		StartCoroutine(CooldownRoutine());
	}

	private IEnumerator CooldownRoutine()
	{
		while (cooldownTimer > 0)
		{
			cooldownTimer -= Time.deltaTime;
			cooldownImage.fillAmount = 1 - (cooldownTimer / cooldownTime);
			yield return null;
		}
		isCooldown = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			shooting();
		}

		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			superShooting();
		}

		if (!isReloading)
		{
			reloadGun();
		}
	}
	private void shooting()
	{
		if (Time.time > shootingSpeed && !isReloading)
		{
			if (bulletsInClip > 0)
			{
				//muzzleFlash.Play();
				_source.GenerateImpulse();
				shootingSpeed = Time.time + maxShootingSpeed;
				audioSource.pitch = Random.Range(0.9f, 1.4f);
				audioSource.PlayOneShot(Shoot);
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

	private void superShooting()
	{
		if (Time.time > shootingSpeed && !isReloading && !isCooldown)
		{
			if (bulletsInClip > 0)
			{
				//muzzleFlash.Play();
				_source.GenerateImpulse();
				shootingSpeed = Time.time + maxShootingSpeed;
				audioSource.pitch = Random.Range(0.9f, 1.4f);
				audioSource.PlayOneShot(Shoot);
				GameObject obj = pool.GetObject(5);
				if (obj != null)
				{
					obj.SetActive(false);
					obj.transform.position = shootingPoint.position;
					obj.transform.rotation = shootingPoint.rotation;
					obj.SetActive(true);
				}
				bulletsInClip--;
				makeNoise();

				// Start cooldown after shooting
				StartCooldown();
			}
			else
			{
				audioSource.PlayOneShot(click);
			}
		}
	}

	/*	private void superShooting()
		{
			if (Time.time > shootingSpeed && !isReloading && //new VAR HERE for cooldown)
			{
				if (bulletsInClip > 0)
				{
					//muzzleFlash.Play();
					_source.GenerateImpulse();
					shootingSpeed = Time.time + maxShootingSpeed;
					audioSource.pitch = Random.Range(0.9f, 1.4f);
					audioSource.PlayOneShot(Shoot);
					GameObject obj = pool.GetObject(5);
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
		}*/

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
	void reloadGun()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
			if (bulletsInClip < maxBulletsInClip)
			{
				audioSource.PlayOneShot(reloadSound);
				isReloading = true;
				StartCoroutine(reload());

				IEnumerator reload()
				{
					yield return new WaitForSeconds(0.5f);
					bulletsInClip = maxBulletsInClip;
					isReloading = false;
				}
			}
		}
	}
}
