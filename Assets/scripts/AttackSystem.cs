using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSystem : MonoBehaviour
{
	//[SerializeField] Animator anim;
	float shootingSpeed;
	[SerializeField] float maxShootingSpeed = 0.5f;
	[SerializeField] GameObject hitCollider;
	GameObjectPool pool;
	[SerializeField] int objectIndex;
	public int bulletsInClip = 5;
	[SerializeField] Transform shootingPoint;
	[SerializeField] ParticleSystem slash;
	AudioSource audioSource;
	[SerializeField] AudioClip click;
	[SerializeField] AudioClip Shoot;
	[SerializeField] AudioClip meleAttackClip;
	[SerializeField] string[] batonAttacks;
	private bool batonAttacking = false;
	[SerializeField] ParticleSystem muzzleFlash;
	//meleHitCollider
	[SerializeField] AudioSource meleHitCol;

	private void Start()
	{
		pool = FindObjectOfType<GameObjectPool>();
		audioSource = GetComponent<AudioSource>();
		batonAttacks = new string[] { "baton", "baton1", "baton2" };
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Mouse0))
		{
			shooting();
		}
/*		if (Input.GetKeyDown(KeyCode.Mouse1))
		{
			if(!batonAttacking)
			StartCoroutine(meleAttack());
		}*/
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
			}
			else
			{
				audioSource.PlayOneShot(click);
			}
		}
	}
/*	IEnumerator meleAttack()
	{
		if (!batonAttacking)
		{
			string playAnim = batonAttacks[Random.Range(0, batonAttacks.Length)];
			//anim.SetTrigger(playAnim);
			batonAttacking = true;
		}
		if (!meleHitCol.isPlaying)
		{
			meleHitCol.clip = null;
		}
		slash.Play();
		audioSource.pitch = Random.Range(0.7f, 1.2f);
		audioSource.PlayOneShot(meleAttackClip);
		hitCollider.SetActive(true);
		yield return new WaitForSeconds(0.2f);
		hitCollider.SetActive(false);
		batonAttacking = false;
	}*/
}
