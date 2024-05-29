using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playSoundOnEnable : MonoBehaviour
{
	AudioSource _audiosource;
	[SerializeField] AudioClip[] clip;

	private void Awake()
	{
		_audiosource = GetComponent<AudioSource>();
	}

	private void OnEnable()
	{
		_audiosource.clip = clip[Random.Range(0, clip.Length)];
		_audiosource.Play();
	}
}
